using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class ANDElementUserControl : UserControl, IElementUserControl
    {
        public List<ConnectionPinUserControl> Pins { get; set; }

        public ANDElementUserControl()
        {
            InitializeComponent();

            Pins = new List<ConnectionPinUserControl> { OutputPin, InputPin1, InputPin2 };
        }
    }
}
