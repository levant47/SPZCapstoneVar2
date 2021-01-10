using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class ANDElementUserControl : UserControl, IElementUserControl
    {
        public ANDElementUserControl(MouseButtonEventHandler connectionMouseLeftButtonDownHandler)
        {
            InitializeComponent();

            InputPin1.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
            InputPin2.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
            OutputPin.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
        }

        public List<ConnectionPinUserControl> GetConnectionPins() => new List<ConnectionPinUserControl> { InputPin1, InputPin2, OutputPin };
    }
}
