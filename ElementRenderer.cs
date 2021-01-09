using SPZCapstoneVar2.Models;
using SPZCapstoneVar2.UserControls;
using System;
using System.Windows;
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
            ElementType.OR_GATE => new ORElementUserControl { RenderTransform = new TranslateTransform(element.PositionX, element.PositionY) },
            ElementType.NOT_GATE => new NOTElementUserControl { RenderTransform = new TranslateTransform(element.PositionX, element.PositionY) },
            _ => throw new NotImplementedException(),
        };
    }
}
