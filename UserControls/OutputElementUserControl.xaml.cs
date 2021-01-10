using System.Collections.Generic;
using System.Windows.Controls;

namespace SPZCapstoneVar2.UserControls
{
    public partial class OutputElementUserControl : UserControl, IElementUserControl
    {
        private bool? _value = null;
        public bool? Value
        {
            get => _value;
            set
            {
                _value = value;
                if (value == null)
                {
                    ValueLabel.Content = "";
                }
                else if (value == true)
                {
                    ValueLabel.Content = "1";
                }
                else
                {
                    ValueLabel.Content = "0";
                }
            }
        }

        public OutputElementUserControl()
        {
            InitializeComponent();
        }

        public List<ConnectionPinUserControl> GetConnectionPins() => new List<ConnectionPinUserControl> { InputPin };

        public List<ConnectionPinUserControl> GetInputConnectionPins() => new List<ConnectionPinUserControl> { InputPin };
    }
}
