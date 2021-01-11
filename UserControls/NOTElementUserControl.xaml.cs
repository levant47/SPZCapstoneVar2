using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class NOTElementUserControl : UserControl, IElementUserControl
    {
        public List<ConnectionPinUserControl> Pins { get; set; }

        public NOTElementUserControl()
        {
            InitializeComponent();

            Pins = new List<ConnectionPinUserControl> { OutputPin, InputPin };

            OutputPin.IsInverted = true;
        }
    }
}
