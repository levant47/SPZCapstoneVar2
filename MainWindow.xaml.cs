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
using System.Windows.Shapes;

namespace SPZCapstoneVar2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitializeElementPanel();

            DesignCanvas.AllowDrop = true;
            DesignCanvas.DragOver += HandleDesignFrameDragOver;
            DesignCanvas.PreviewDrop += HandleDesignFrameDrop;
            DesignCanvas.MouseMove += HandleDesignCanvasMouseMove;
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

        private void HandleDesignCanvasMouseMove(object sender, MouseEventArgs eventArgs)
        {
            DesignCanvas.Children
                .Cast<object>()
                .Where(child => child is IElementUserControl)
                .Cast<IElementUserControl>()
                .SelectMany(elementUserControl => elementUserControl.GetConnectionPins())
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

        private void HandleConnectionMouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
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
                var doesWireConnectToAnInputPin = DesignCanvas.Children
                    .Cast<object>()
                    .Where(child => child is IElementUserControl)
                    .Cast<IElementUserControl>()
                    .SelectMany(elementUserControl => elementUserControl.GetInputConnectionPins())
                    .Any(connectionPin => connectionPin.InputHitTest(Mouse.GetPosition(connectionPin)) != null);
                if (!doesWireConnectToAnInputPin)
                {
                    DesignCanvas.Children.Remove(wire);
                }
            }
            DesignCanvas.MouseLeftButtonUp += dragStopHandler;
            DesignCanvas.Children.Add(wire);
        }

        private void HandleDesignFrameDrop(object sender, DragEventArgs eventArgs)
        {
            var elementType = Enum.Parse<ElementType>((string)eventArgs.Data.GetData(DataFormats.Text));
            var mousePosition = eventArgs.GetPosition(DesignCanvas);
            DesignCanvas.Children.Add(ElementRenderer.Render(HandleConnectionMouseLeftButtonDown, new Element
            {
                Type = elementType,
                PositionX = mousePosition.X,
                PositionY = mousePosition.Y,
            }));
        }
    }
}
