using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                OnPropertyChanged("PosZudah");
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
                OnPropertyChanged("PosKampfer");
            }
        }

        private Image? MovingImage = null;
        private Boolean IsMoving = false;

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

            switch (MovingImage?.Name)
            {
                case "Zudah": 
                    PosZudah = e.GetPosition(this); 
                    break;
                case "Kampfer":
                    PosKampfer = e.GetPosition(this);
                    break;
                default:
                    break;
            }
        }
    }

    public class Center : IValueConverter
    {
        const double RADIUS = 50;

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
