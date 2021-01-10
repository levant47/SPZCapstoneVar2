using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class ORElementUserControl : UserControl, IElementUserControl
    {
        public ORElementUserControl(MouseButtonEventHandler connectionMouseLeftButtonDownHandler)
        {
            InitializeComponent();

            OutputPin.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
        }

        public List<ConnectionPinUserControl> GetConnectionPins() => new List<ConnectionPinUserControl> { InputPin1, InputPin2, OutputPin };

        public List<ConnectionPinUserControl> GetInputConnectionPins() => new List<ConnectionPinUserControl> { InputPin1, InputPin2 };
    }
}
