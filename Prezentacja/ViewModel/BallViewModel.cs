using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Prezentacja.ViewModel
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private double x;
        private double y;
        private double radius;

        public double X
        {
            get => x;
            set { x = value; OnPropertyChanged(); }
        }

        public double Y
        {
            get => y;
            set { y = value; OnPropertyChanged(); }
        }

        public double Radius
        {
            get => radius;
            set { radius = value; OnPropertyChanged(); }
        }

        public BallViewModel(double x, double y, double radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public void UpdatePosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
