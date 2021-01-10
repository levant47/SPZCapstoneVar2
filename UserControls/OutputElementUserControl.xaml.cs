using System.Collections.Generic;
using System.Windows.Controls;

namespace SPZCapstoneVar2.UserControls
{
    public partial class OutputElementUserControl : UserControl, IElementUserControl
    {
        public OutputElementUserControl()
        {
            InitializeComponent();
        }

        public List<ConnectionPinUserControl> GetConnectionPins() => new List<ConnectionPinUserControl> { InputPin };

        public List<ConnectionPinUserControl> GetInputConnectionPins() => new List<ConnectionPinUserControl> { InputPin };
    }
}
