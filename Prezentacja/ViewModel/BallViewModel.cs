using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logika;

namespace Prezentacja.ViewModel
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private ILogic ball;

        public double X => ball.Data.X;
        public double Y => ball.Data.Y;
        public double Radius => ball.Data.Radius;

        public BallViewModel(ILogic ball)
        {
            this.ball = ball;
        }

        public void Update()
        {
            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }

        public ILogic Logic => ball;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
