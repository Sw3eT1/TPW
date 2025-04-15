using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Logika;
using System.Windows.Media;
using Prezentacja.ViewModel;
using Dane;

namespace Prezentacja.Model
{
    public class MainModel
    {
        private ObservableCollection<BallViewModel> balls;
        public ObservableCollection<BallViewModel> Balls => balls;
        private Random rand = new Random();
        private int width;
        private int height;
        private List<ILogic> logics = new();
        private DispatcherTimer moveTimer;

        public MainModel()
        {
            balls = new ObservableCollection<BallViewModel>();
            moveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20) };
            moveTimer.Tick += MoveBalls;
        }

        public void StartSimulation(int ballCount, int width, int height)
        {
            balls.Clear();
            this.width = width;
            this.height = height;
            SpawnBalls(ballCount);
            moveTimer.Start();

        }

        public void StopSimulation()
        {
            moveTimer.Stop();
            balls.Clear();
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
            }
        }



        private void MoveBalls(object sender, EventArgs e)
        {
           for(int i = 0; i< logics.Count; i++)
            {
                logics[i].Move(width, height);
                balls[i].UpdatePosition(logics[i].Data.X, logics[i].Data.Y);
            }

            CheckCollisions();
        }


        private void CheckCollisions()
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