using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class NOTElementUserControl : UserControl, IElementUserControl
    {
        public NOTElementUserControl(MouseButtonEventHandler connectionMouseLeftButtonDownHandler)
        {
            InitializeComponent();

            OutputPin.MouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
            OutputPin.IsInverted = true;
        }

        public List<ConnectionPinUserControl> GetConnectionPins() => new List<ConnectionPinUserControl> { InputPin, OutputPin };

        public List<ConnectionPinUserControl> GetInputConnectionPins() => new List<ConnectionPinUserControl> { InputPin };
    }
}
