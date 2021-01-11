using SPZCapstoneVar2.Models;
using SPZCapstoneVar2.UserControls;
using SPZCapstoneVar2.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SPZCapstoneVar2
{
    public partial class MainWindow : Window
    {
        private int NextId = 1;
        private readonly Dictionary<UIElement, Element> _elements = new Dictionary<UIElement, Element>();
        private readonly Dictionary<UIElement, Connection> _connections = new Dictionary<UIElement, Connection>();

        public MainWindow()
        {
            InitializeComponent();

            InitializeElementPanel();

            DesignCanvas.AllowDrop = true;
            DesignCanvas.DragOver += HandleDesignFrameDragOver;
            DesignCanvas.PreviewDrop += HandleDesignFrameDrop;
            DesignCanvas.MouseMove += HandleDesignCanvasMouseMove;
            DesignCanvas.MouseRightButtonDown += HandleRightButtonDown;
            DesignCanvas.MouseLeftButtonDown += HandleMouseLeftButtonDown;
        }

        private void InitializeElementPanel()
        {
            Enum.GetValues(typeof(ElementType))
                .Cast<Enum>()
                .ForEach(variant =>
                {
                    var description = variant.GetDescription()
                        ?? throw new Exception($"Variant {variant} of {nameof(ElementType)} enum has no description defined");
                    var item = new ListBoxItem { Content = description, DataContext = variant.ToString() };
                    item.PreviewMouseLeftButtonDown += HandleElementListItemDragEnter;
                    ElementList.Items.Add(item);
                });
        }

        private void HandleRightButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            var targetElementUserControl = DesignCanvas.Children.Cast<UIElement>()
                .Where(uiElement => uiElement is IElementUserControl)
                .FirstOrDefault(elementUserControl => elementUserControl.InputHitTest(Mouse.GetPosition(elementUserControl)) != null);
            if (targetElementUserControl != null)
            {
                var contextMenu = (FindResource("elementContextMenu") as ContextMenu)!;
                var targetElement = _elements[targetElementUserControl];
                contextMenu.PlacementTarget = targetElementUserControl;
                contextMenu.IsOpen = true;
                contextMenu.DataContext = targetElementUserControl;
                contextMenu.Items.Cast<MenuItem>().ForEach(menuItem =>
                    menuItem.Visibility = menuItem.Name == "ConfigureNumberOfPinsMenuItem"
                            && new[] { ElementType.INPUT_ELEMENT, ElementType.OUTPUT_ELEMENT, ElementType.NOT_GATE }
                                .Contains(targetElement.Type)
                        ? Visibility.Visible
                        : Visibility.Collapsed);
                return;
            }
        }

        private void HandleDesignCanvasMouseMove(object sender, MouseEventArgs eventArgs)
        {
            DesignCanvas.Children
                .Cast<object>()
                .Where(child => child is IElementUserControl)
                .Cast<IElementUserControl>()
                .SelectMany(elementUserControl => elementUserControl.Pins)
                .ForEach(connectionPin =>
                {
                    if (connectionPin.InputHitTest(Mouse.GetPosition(connectionPin)) != null)
                    {
                        if (!connectionPin.IsHighlighted)
                        {
                            connectionPin.IsHighlighted = true;
                        }
                    }
                    else
                    {
                        if (connectionPin.IsHighlighted)
                        {
                            connectionPin.IsHighlighted = false;
                        }
                    }
                });
        }

        private void HandleElementListItemDragEnter(object sender, MouseButtonEventArgs eventArgs)
        {
            var item = (ListBoxItem)sender;
            DragDrop.DoDragDrop(item, item.DataContext, DragDropEffects.Move);
        }

        private void HandleDesignFrameDragOver(object sender, DragEventArgs eventArgs)
        {
            eventArgs.Effects = DragDropEffects.Move;
        }

        private void HandleConnectionMouseLeftButtonDown1(object sender, MouseButtonEventArgs eventArgs)
        {
        }

        private void HandleMouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            var targetElementUserControl = DesignCanvas.Children.Cast<UIElement>()
                .Where(uiElement => uiElement is IElementUserControl)
                .FirstOrDefault(elementUserControl => elementUserControl.InputHitTest(Mouse.GetPosition(elementUserControl)) != null);
            if (targetElementUserControl == null)
            {
                return;
            }

            // creating a wire
            var originPin = (targetElementUserControl as IElementUserControl)!.Pins
                .FirstOrDefault(connectionPin => connectionPin.InputHitTest(Mouse.GetPosition(connectionPin)) != null);
            if (originPin != null)
            {
                StartWireCreation(targetElementUserControl, originPin);
                return;
            }

            // moving an element
            StartElementMove(targetElementUserControl);
        }

        private void StartElementMove(UIElement targetElementUserControl)
        {
            var targetElementId = _elements[targetElementUserControl].Id;
            MouseEventHandler handleMouseMove = (object sender, MouseEventArgs eventArgs) =>
            {
                // move the element
                var origin = targetElementUserControl.RenderTransformOrigin;
                var mousePosition = Mouse.GetPosition(DesignCanvas);
                var difference = new Point(mousePosition.X - origin.X, mousePosition.Y - origin.Y);
                targetElementUserControl.RenderTransform = new TranslateTransform(difference.X, difference.Y);

                // move its wires
                var elementId = _elements[targetElementUserControl].Id;
                _connections.Where(wireConnectionPair => wireConnectionPair.Value.IsConnectedTo(elementId))
                    .ForEach(wireConnectionPair =>
                    {
                        var (wire, connection) = ((wireConnectionPair.Key as WireUserControl)!, wireConnectionPair.Value);
                        var pinIndex = connection.FromId == targetElementId ? connection.FromPinIndex : connection.ToPinIndex;
                        var pin = (targetElementUserControl as IElementUserControl)!.Pins[pinIndex];
                        var newPinLocation = DesignCanvas.TranslatePoint(new Point(), pin);
                        if (connection.FromId == targetElementId)
                        {
                            wire.RebaseTo(new Point(-newPinLocation.X, -newPinLocation.Y));
                        }
                        else
                        {
                            wire.PointTo(new Point(-newPinLocation.X, -newPinLocation.Y));
                        }
                    });
            };
            DesignCanvas.MouseMove += handleMouseMove;

            var originalCursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Hand;
            DesignCanvas.MouseUp += (object sender, MouseButtonEventArgs e) =>
            {
                DesignCanvas.MouseMove -= handleMouseMove;
                Mouse.OverrideCursor = originalCursor;
            };
        }

        private void StartWireCreation(UIElement originElement, ConnectionPinUserControl originPin)
        {
            var originElementId = _elements[originElement].Id;
            var wire = new WireUserControl(Mouse.GetPosition(DesignCanvas));
            MouseEventHandler dragHandler = (object _sender1, MouseEventArgs eventArgs1) =>
            {
                wire.PointTo(eventArgs1.GetPosition(DesignCanvas));
            };
            DesignCanvas.MouseMove += dragHandler;
            void dragStopHandler(object _sender2, MouseButtonEventArgs eventArgs2)
            {
                DesignCanvas.MouseMove -= dragHandler;
                DesignCanvas.MouseLeftButtonUp -= dragStopHandler;
                var destinationPin = DesignCanvas.Children
                    .Cast<object>()
                    .Where(child => child is IElementUserControl)
                    .Cast<IElementUserControl>()
                    .SelectMany(elementUserControl => elementUserControl.Pins.Skip(1))
                    .FirstOrDefault(connectionPin => connectionPin.InputHitTest(Mouse.GetPosition(connectionPin)) != null);
                if (destinationPin == null)
                {
                    DesignCanvas.Children.Remove(wire);
                    return;
                }

                var destinationElement = DesignCanvas.Children
                    .Cast<object>()
                    .Where(child => child is IElementUserControl)
                    .Cast<UIElement>()
                    .First(elementUserControl => elementUserControl.InputHitTest(Mouse.GetPosition(elementUserControl)) != null);
                var destinationElementId = _elements[destinationElement].Id;

                _connections.Add(wire, new Connection
                {
                    FromId = originElementId,
                    ToId = destinationElementId,
                    FromPinIndex = (originElement as IElementUserControl)!.Pins.IndexOf(originPin),
                    ToPinIndex = (destinationElement as IElementUserControl)!.Pins.IndexOf(destinationPin),
                });
            }
            DesignCanvas.MouseLeftButtonUp += dragStopHandler;
            DesignCanvas.Children.Add(wire);
        }

        private void HandleDesignFrameDrop(object sender, DragEventArgs eventArgs)
        {
            var elementType = Enum.Parse<ElementType>((string)eventArgs.Data.GetData(DataFormats.Text));
            var mousePosition = eventArgs.GetPosition(DesignCanvas);
            var newElement = new Element
            {
                Id = NextId,
                Type = elementType,
                PositionX = mousePosition.X,
                PositionY = mousePosition.Y,
            };
            NextId++;
            var renderedElement = ElementRenderer.Render(HandleConnectionMouseLeftButtonDown1, newElement);
            _elements.Add(renderedElement, newElement);
            DesignCanvas.Children.Add(renderedElement);
        }

        private void SimulateButton_Click(object sender, RoutedEventArgs e)
        {
            var schemaSimulation = new SchemaSimulation(_elements.Values.ToList(), _connections.Values.ToList());
            DesignCanvas.Children
                .Cast<UIElement>()
                .Where(uiElement => uiElement is OutputElementUserControl)
                .Cast<OutputElementUserControl>()
                .ForEach(outputElement =>
                {
                    outputElement.Value = schemaSimulation.CalculateValueFor(_elements[outputElement]);
                });
        }

        private void ContextMenuRemoveOptionClick(object sender, RoutedEventArgs eventArgs)
        {
            var targetElementUserControl = ((sender as MenuItem)!.DataContext as UIElement)!;
            var targetElementId = _elements[targetElementUserControl].Id;

            // remove the element itself
            _elements.Remove(targetElementUserControl);
            DesignCanvas.Children.Remove(targetElementUserControl);

            // remove all incoming and outgoing connections of the removed element
            _connections.Where(wireConnectionPair => wireConnectionPair.Value.ToId == targetElementId
                    || wireConnectionPair.Value.FromId == targetElementId)
                .ForEach(wireConnectionPair =>
                {
                    _connections.Remove(wireConnectionPair.Key);
                    DesignCanvas.Children.Remove(wireConnectionPair.Key);
                });
        }

        private void HandleContextMenuOptionConfigureNumberOfPinsClick(object sender, RoutedEventArgs e)
        {
            var targetUserControl = ((sender as MenuItem)!.DataContext as IElementUserControl)!;
            var targetUIElement = (targetUserControl as UIElement)!;
            var targetElement = _elements[targetUIElement];
            new ElementConfigurationWindow(targetUserControl.Pins.Count - 1, newNumberOfPins =>
            {
                // targetUserControl.ConnectionPinCount = newNumberOfPins;
                _connections
                    .Where(wireConnectionPair => wireConnectionPair.Value.ToId == targetElement.Id
                        // +1 is accounting for the output pin which is supposed to always be the first one in the list
                        && (wireConnectionPair.Value.ToPinIndex + 1) >= newNumberOfPins)
                    .ForEach(wireConnectionPair => _connections.Remove(wireConnectionPair.Key));
            });
        }
    }
}
