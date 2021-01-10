using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SPZCapstoneVar2.UserControls
{
    public partial class WireUserControl : UserControl
    {
        private readonly Point _startPosition;

        public WireUserControl(Point startPosition)
        {
            InitializeComponent();

            _startPosition = startPosition;

            RenderTransform = new TranslateTransform(startPosition.X, startPosition.Y);
            Canvas.SetLeft(this, startPosition.X);
            Canvas.SetTop(this, startPosition.Y);
        }

        public void PointTo(Point newPosition)
        {
            RenderTransform = new ScaleTransform(newPosition.X - _startPosition.X, 1);
        }
    }
}
