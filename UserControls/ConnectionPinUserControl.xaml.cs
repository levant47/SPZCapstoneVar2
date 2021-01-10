using System.Windows.Controls;
using System.Windows.Media;

namespace SPZCapstoneVar2.UserControls
{
    public partial class ConnectionPinUserControl : UserControl
    {
        private static readonly Brush HighlightBrush = new SolidColorBrush(Colors.Green);
        private static readonly Brush NoHighlightBrush = new SolidColorBrush(Colors.Black);

        private bool _isHighlighted = false;
        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                _isHighlighted = value;
                var currentBrush = value ? HighlightBrush : NoHighlightBrush;
                Point.Stroke = currentBrush;
                Point.Fill = currentBrush;
            }
        }
        public ConnectionPinUserControl()
        {
            InitializeComponent();
        }
    }
}
