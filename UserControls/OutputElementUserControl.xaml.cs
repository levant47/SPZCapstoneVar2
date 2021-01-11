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

        public List<ConnectionPinUserControl> Pins { get; set; }

        public OutputElementUserControl()
        {
            InitializeComponent();

            // haxx
            Pins = new List<ConnectionPinUserControl> { new ConnectionPinUserControl(), InputPin };
        }
    }
}
