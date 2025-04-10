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
            for (int i = 0; i < count; i++)
            {
                IShape data = BallData.CreateRandomShape(width, height);
                var logic = new BallLogic { Data = data as BallData };
                var ballVM = new BallViewModel(logic);
                balls.Add(ballVM);
            }
        }



        private void MoveBalls(object sender, EventArgs e)
        {
            foreach (var ball in balls)
            {
                ball.Logic.Move(width, height);
                ball.Update();
            }

            CheckCollisions();
        }


        private void CheckCollisions()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    if (balls[i].Logic.CheckCollision(balls[j].Logic))
                    {
                        balls[i].Logic.ResolveCollision(balls[j].Logic);
                    }
                }
            }
        }

    }
}