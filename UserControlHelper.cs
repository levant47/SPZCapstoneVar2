using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SPZCapstoneVar2
{
    public static class UserControlHelper
    {
        public static void HighlightConnectionOnMouseEnter(object sender, MouseEventArgs eventArgs)
        {
            ((Ellipse)sender).Stroke = new SolidColorBrush(Colors.Green);
        }

        public static void DehighlightConnectionOnMouseLeave(object sender, MouseEventArgs eventArgs)
        {
            ((Ellipse)sender).Stroke = new SolidColorBrush(Colors.Black);
        }
    }
}
