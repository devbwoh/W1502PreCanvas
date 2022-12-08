using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        const double RADIUS = 50;

        public double CenterX { get; set; }
        public double CenterY { get; set; }

        // prop 탭 2번
        //public double PosX { get; set; }
        //public double PosY { get; set; }

        // propfull 탭 2번
        private Point _pos;

        public Point Pos
        {
            get { return _pos; }
            set { 
                _pos = value;
                CenterX = _pos.X - RADIUS;
                CenterY = _pos.Y - RADIUS;
                OnPropertyChanged("Pos");
                OnPropertyChanged("CenterX");
                OnPropertyChanged("CenterY");
            }
        }

        private Boolean IsMoving = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //PosX = 200;
            //PosY = 200;
            Pos = new Point(200, 200);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("마우스 버튼 Down");
            IsMoving = true;
        }
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("마우스 버튼 Up");
            IsMoving = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMoving)
                return;
            Pos = e.GetPosition(this);

        }
    }
}
