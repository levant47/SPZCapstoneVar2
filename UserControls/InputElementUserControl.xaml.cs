using SPZCapstoneVar2.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class InputElementUserControl : UserControl, IElementUserControl
    {
        private readonly Element _sourceElement;

        public bool Value { get; private set; } = false;
        public List<ConnectionPinUserControl> Pins { get; set; }

        public InputElementUserControl(MouseButtonEventHandler connectionMouseLeftButtonDownHandler, Element sourceElement)
        {
            InitializeComponent();

            _sourceElement = sourceElement;
            Pins = new List<ConnectionPinUserControl> { OutputPin };

            OutputPin.PreviewMouseLeftButtonDown += connectionMouseLeftButtonDownHandler;

            MouseDoubleClick += HandleMousDoubleClick;
        }

        private void HandleMousDoubleClick(object sender, MouseButtonEventArgs eventArgs)
        {
            var answer = MessageBox.Show("Click Yes for 1 and No for 0", "Choose new value", MessageBoxButton.YesNo);
            Value = answer == MessageBoxResult.Yes;
            _sourceElement.InputElementValue = Value;
            ValueLabel.Content = Value ? "1" : "0";
        }
    }
}
