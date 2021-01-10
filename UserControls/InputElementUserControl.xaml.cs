using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class InputElementUserControl : UserControl, IElementUserControl
    {
        public bool Value { get; private set; } = false;

        public InputElementUserControl(MouseButtonEventHandler connectionMouseLeftButtonDownHandler)
        {
            InitializeComponent();

            OutputPin.PreviewMouseLeftButtonDown += connectionMouseLeftButtonDownHandler;

            MouseDoubleClick += HandleMousDoubleClick;
        }

        private void HandleMousDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            var answer = MessageBox.Show("Click Yes for 1 and No for 0", "Choose new value", MessageBoxButton.YesNo);
            Value = answer == MessageBoxResult.Yes;
            ValueLabel.Content = Value ? "1" : "0";
        }

        public List<ConnectionPinUserControl> GetConnectionPins() => new List<ConnectionPinUserControl> { OutputPin };

        public List<ConnectionPinUserControl> GetInputConnectionPins() => new List<ConnectionPinUserControl>();
    }
}
