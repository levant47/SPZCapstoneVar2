using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class XNORElementUserControl : UserControl, IElementUserControl
    {
        public List<ConnectionPinUserControl> Pins { get; set; }

        public XNORElementUserControl(MouseButtonEventHandler connectionMouseLeftButtonDownHandler)
        {
            InitializeComponent();

            Pins = new List<ConnectionPinUserControl> { OutputPin, InputPin1, InputPin2 };

            OutputPin.PreviewMouseLeftButtonDown += connectionMouseLeftButtonDownHandler;
        }
    }
}
