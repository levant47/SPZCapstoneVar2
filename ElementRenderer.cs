using SPZCapstoneVar2.Models;
using SPZCapstoneVar2.UserControls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SPZCapstoneVar2
{
    public static class ElementRenderer
    {
        public static UIElement Render(MouseButtonEventHandler connectionMouseLeftButtonDownHandler, Element element) => element.Type switch
        {
            ElementType.AND_GATE => new ANDElementUserControl(connectionMouseLeftButtonDownHandler)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            ElementType.OR_GATE => new ORElementUserControl(connectionMouseLeftButtonDownHandler)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            ElementType.NOT_GATE => new NOTElementUserControl(connectionMouseLeftButtonDownHandler)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            ElementType.INPUT_ELEMENT => new InputElementUserControl(connectionMouseLeftButtonDownHandler, element)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            ElementType.OUTPUT_ELEMENT => new OutputElementUserControl { RenderTransform = new TranslateTransform(element.PositionX, element.PositionY) },
            ElementType.XOR_GATE => new XORElementUserControl(connectionMouseLeftButtonDownHandler)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            ElementType.NAND_GATE => new NANDElementUserControl(connectionMouseLeftButtonDownHandler)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            ElementType.NOR_GATE => new NORElementUserControl(connectionMouseLeftButtonDownHandler)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            ElementType.XNOR_GATE => new XNORElementUserControl(connectionMouseLeftButtonDownHandler)
            {
                RenderTransform = new TranslateTransform(element.PositionX, element.PositionY),
            },
            _ => throw new NotImplementedException(),
        };

        public static void AddPin(IElementUserControl elementUserControl)
        {
            var pin1 = elementUserControl.Pins[1]; // accounting for the output pin
            var pin2 = elementUserControl.Pins[2];

            var ui = (elementUserControl as UserControl)!;
            var grid = (ui.Content as Grid)!;

            var newPin = new ConnectionPinUserControl();
            var pin1Pos = ui.TranslatePoint(new Point(), pin1) * new Matrix(-1, 0, 0, -1, 0, 0);
            var pin2Pos = ui.TranslatePoint(new Point(), pin2) * new Matrix(-1, 0, 0, -1, 0, 0);
            var topPin = pin1Pos.Y < pin2Pos.Y ? pin1Pos : pin2Pos;
            var step = Math.Abs(pin2Pos.Y - pin1Pos.Y) / 7;
            var newPinPos = new Point(topPin.X, topPin.Y + step * (elementUserControl.Pins.Count - 2));
            newPin.RenderTransform = new TranslateTransform(newPinPos.X - ui.ActualWidth / 2 + 3, newPinPos.Y - ui.ActualHeight / 2 + 3);

            grid.Children.Add(newPin);
            elementUserControl.Pins.Add(newPin);
        }

        public static void RemovePin(IElementUserControl elementUserControl)
        {
            var lastPin = elementUserControl.Pins.Last();
            ((elementUserControl as UserControl)!.Content as Grid)!.Children.Remove(lastPin);
            elementUserControl.Pins.Remove(lastPin);
        }
    }
}
