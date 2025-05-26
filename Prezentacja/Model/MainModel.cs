using System.Collections.ObjectModel;
using System.Windows.Threading;
using Dane;
using Logika;
using Prezentacja.ViewModel;


namespace Prezentacja.Model
{
    public class MainModel
    {
        private ObservableCollection<BallViewModel> balls;
        public ObservableCollection<BallViewModel> Balls => balls;
        private int width;
        private int height;
        private List<ILogic> logics = new();
        private Dispatcher dispatcher;
        private CancellationTokenSource simulationTokenSource;

        public MainModel(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            balls = new ObservableCollection<BallViewModel>();
        }

        public void StartSimulation(int ballCount, int width, int height, Dispatcher dispatcher)
        {

            Logger.Instance.StartLogging();
            Logger.Instance.Log("--- Simulation Started ---");

            balls.Clear();
            this.width = width;
            this.height = height;
            SpawnBalls(ballCount);

            simulationTokenSource = new CancellationTokenSource();
            var token = simulationTokenSource.Token;

            foreach (var logic in logics)
            {
                logic.SimulateMove(width, height);
            }
        }

        private void SpawnBalls(int count)
        {
            balls.Clear();
            logics.Clear();

            for (int i = 0; i < count; i++)
            {
                ILogic logic = new BallLogic(width, height);
                logics.Add(logic);

                var ballVM = new BallViewModel(logic.Data.X, logic.Data.Y, logic.Data.Radius);
                balls.Add(ballVM);

                logic.Data.PositionChanged += (x, y) =>
                {
                    dispatcher.Invoke(() => ballVM.UpdatePosition(x, y));
                    CheckCollisions();
                };
            }
        }

        public void StopSimulation()
        {
            simulationTokenSource?.Cancel();

            foreach (var logic in logics)
            {
                logic.Data?.Stop();
            }

            balls.Clear();
            logics.Clear();

            Logger.Instance.Log("--- Simulation Stopped ---");
            Logger.Instance.StopLogging();

        }

        private readonly object collisionLock = new();
        private void CheckCollisions()
        {
            lock (collisionLock)
            {
                for (int i = 0; i < logics.Count; i++)
                {
                    for (int j = i + 1; j < logics.Count; j++)
                    {
                        if (logics[i].CheckCollision(logics[j]))
                        {
                            logics[i].ResolveCollision(logics[j]);
                        }
                    }
                }
            }
        }


    }
}