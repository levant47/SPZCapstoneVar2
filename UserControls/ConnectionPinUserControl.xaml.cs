using System.Windows.Controls;
using System.Windows.Media;

namespace SPZCapstoneVar2.UserControls
{
    public partial class ConnectionPinUserControl : UserControl
    {
        private static readonly Brush HighlightBrush = new SolidColorBrush(Colors.Green);
        private static readonly Brush NoHighlightBrush = new SolidColorBrush(Colors.Black);
        private static readonly Brush InvertedBrush = new SolidColorBrush(Colors.White);
        private static readonly Brush NotInvertedBrush = new SolidColorBrush(Colors.Black);

        private bool _isHighlighted = false;
        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                _isHighlighted = value;
                var currentBrush = value ? HighlightBrush : NoHighlightBrush;
                Point.Stroke = currentBrush;
                Point.Fill = IsInverted ? InvertedBrush : currentBrush;
            }
        }

        private bool _isInverted = false;
        public bool IsInverted
        {
            get => _isInverted;
            set
            {
                _isInverted = value;
                var currentBrush = value ? InvertedBrush : NotInvertedBrush;
                Point.Fill = currentBrush;
            }
        }

        public ConnectionPinUserControl()
        {
            InitializeComponent();
        }
    }
}
