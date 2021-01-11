using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SPZCapstoneVar2.UserControls
{
    public partial class WireUserControl : UserControl
    {
        private readonly Point _veryFirstPosition;
        private Point _startPosition;
        private Point _currentPosition;

        public WireUserControl(Point startPosition)
        {
            InitializeComponent();

            _veryFirstPosition = startPosition;
            _startPosition = startPosition;

            Horiz1.RenderTransform = new TranslateTransform(startPosition.X, startPosition.Y);
            Vert.RenderTransform = new TranslateTransform(startPosition.X, startPosition.Y);
            Horiz2.RenderTransform = new TranslateTransform(startPosition.X, startPosition.Y);
            Canvas.SetLeft(this, startPosition.X);
            Canvas.SetTop(this, startPosition.Y);
        }

        public void PointTo(Point newPosition)
        {
            _currentPosition = newPosition;
            var connectionWidth = newPosition.X - _startPosition.X;
            var connectionHeight = newPosition.Y - _startPosition.Y;

            // the first horizontal line
            Horiz1.RenderTransform = new ScaleTransform(connectionWidth / 2, 1);

            // the vertical line
            {
                var transformGroup = new TransformGroup();
                transformGroup.Children.Add(new TranslateTransform(connectionWidth / 2, 0));
                transformGroup.Children.Add(new ScaleTransform(1, connectionHeight));
                Vert.RenderTransform = transformGroup;
            }

            // the second horizontal line
            {
                var transformGroup = new TransformGroup();
                transformGroup.Children.Add(new ScaleTransform(connectionWidth / 2, 1));
                transformGroup.Children.Add(new TranslateTransform(connectionWidth / 2, connectionHeight));
                Horiz2.RenderTransform = transformGroup;
            }
        }

        public void RebaseTo(Point newPosition)
        {
            _startPosition = newPosition;
            PointTo(_currentPosition);
            var difference = Point.Subtract(newPosition, _veryFirstPosition);
            RenderTransform = new TranslateTransform(difference.X, difference.Y);
        }
    }
}
