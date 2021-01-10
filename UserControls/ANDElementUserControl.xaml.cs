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

            OutputPin.PreviewMouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
        }

        public List<ConnectionPinUserControl> GetConnectionPins() => new List<ConnectionPinUserControl> { InputPin1, InputPin2, OutputPin };

        public List<ConnectionPinUserControl> GetInputConnectionPins() => new List<ConnectionPinUserControl> { InputPin1, InputPin2 };
    }
}
