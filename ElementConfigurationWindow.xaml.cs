using System;
using System.Windows;

namespace SPZCapstoneVar2
{
    public partial class ElementConfigurationWindow : Window
    {
        private Action<int> _onSubmit;
        public ElementConfigurationWindow(int currentNumberOfPins, Action<int> onSubmit)
        {
            InitializeComponent();

            _onSubmit = onSubmit;

            numberOfPinsTextBox.Text = currentNumberOfPins.ToString();
        }

        private void HandleSubmit(object sender, RoutedEventArgs e)
        {
            var numberOfPinsString = numberOfPinsTextBox.Text;
            var numberOfPinsValid = int.TryParse(numberOfPinsString, out var numberOfPins);
            if (!numberOfPinsValid || numberOfPins < 2 || numberOfPins > 8)
            {
                MessageBox.Show("Invalid number of pins. Must be between 2 and 8.");
                return;
            }
            _onSubmit(numberOfPins);
        }
    }
}
