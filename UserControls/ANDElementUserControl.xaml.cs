using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class ANDElementUserControl : UserControl
    {
        public ANDElementUserControl(MouseButtonEventHandler connectionMouseLeftButtonDownHandler)
        {
            InitializeComponent();

            InputPoint1.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
            InputPoint2.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
            OutputPoint.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
        }
    }
}
