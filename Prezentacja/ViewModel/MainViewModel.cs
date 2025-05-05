using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prezentacja.Model;
using Prezentacja.Commands;
using System.Windows;
using System.Windows.Threading;

namespace Prezentacja.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel model;
        private int ballCount;
        private const int MaxBallCount = 1000;

        public int BallCount
        {
            get { return ballCount; }
            set
            {
                if (value <= MaxBallCount)
                {
                    ballCount = value;
                    OnPropertyChanged();
                }
                else
                {
                    ballCount = MaxBallCount;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<BallViewModel> Balls => model.Balls;


        public ICommand StartCommand { get; }

        public MainViewModel()
        {
            model = new MainModel(Dispatcher.CurrentDispatcher);
            ballCount = 10;
            StartCommand = new Command(StartSimulation);
        }

        private void StartSimulation(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                model.StartSimulation(ballCount, (int)element.ActualWidth, (int)element.ActualHeight, Dispatcher.CurrentDispatcher);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}