using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace W1502PreCanvas
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //const double RADIUS = 50;

        //public double CenterX { get; set; }
        //public double CenterY { get; set; }

        // prop 탭 2번
        //public double PosX { get; set; }
        //public double PosY { get; set; }

        // propfull 탭 2번
        private Point _posZudah;

        public Point PosZudah
        {
            get { return _posZudah; }
            set { 
                _posZudah = value;
                //CenterX = _pos.X - RADIUS;
                //CenterY = _pos.Y - RADIUS;
                OnPropertyChanged(nameof(PosZudah));
                //OnPropertyChanged("CenterX");
                //OnPropertyChanged("CenterY");
            }
        }

        private Point _posKampfer;

        public Point PosKampfer
        {
            get { return _posKampfer; }
            set
            {
                _posKampfer = value;
                OnPropertyChanged(nameof(PosKampfer));
            }
        }

        private Image? MovingImage = null;
        private Boolean IsMoving = false;
        private Point PosDiff = new Point(0, 0);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            PosZudah = new Point(200, 200);
            PosKampfer = new Point(400, 200);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MovingImage = (Image)sender;
            IsMoving = true;

            PosDiff = e.GetPosition(MovingImage);
        }
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            MovingImage = null;
            IsMoving = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMoving)
                return;

            Point p = new Point(
                e.GetPosition(this).X + Center.RADIUS - PosDiff.X,
                e.GetPosition(this).Y + Center.RADIUS - PosDiff.Y);

            switch (MovingImage?.Name)
            {
                case "Zudah":
                    PosZudah = p;
                    break;
                case "Kampfer":
                    PosKampfer = p;
                    break;
                default:
                    break;
            }
        }
    }

    public class Center : IValueConverter
    {
        public const double RADIUS = 50;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - RADIUS;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value + RADIUS;
        }
    }
}
