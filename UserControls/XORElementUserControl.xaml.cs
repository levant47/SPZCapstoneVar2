using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class XORElementUserControl : UserControl, IElementUserControl
    {
        public List<ConnectionPinUserControl> Pins { get; set; }

        public XORElementUserControl()
        {
            InitializeComponent();

            Pins = new List<ConnectionPinUserControl> { OutputPin, InputPin1, InputPin2 };
        }
    }
}
