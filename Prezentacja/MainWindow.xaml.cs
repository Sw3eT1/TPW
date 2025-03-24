using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Logika;

namespace Prezentacja
{
    public partial class MainWindow : Window
    {
        private List<BallLogic> balls = new();
        private Random rand = new();
        private DispatcherTimer moveTimer;
        private int ballCount;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BallCountTextBox.Text, out ballCount))
            {
                ClearBalls();
                SpawnBalls();
                if (moveTimer == null)
                {
                    StartMoveTimer();
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowa liczba kulek.");
            }
        }

        private void SpawnBalls()
        {
            for (int i = 0; i < ballCount; i++)
            {
                BallLogic ball = new(rand, (int)MainCanvas.ActualWidth, (int)MainCanvas.ActualHeight);
                balls.Add(ball);
                CreateBallShape(ball);
            }
        }

        private void ClearBalls()
        {
            MainCanvas.Children.Clear();
            balls.Clear();
        }

        private void CreateBallShape(BallLogic ball)
        {
            Ellipse shape = new Ellipse
            {
                Width = ball.Data.Radius,
                Height = ball.Data.Radius,
                Fill = new SolidColorBrush(Color.FromArgb(ball.Data.Color.A, ball.Data.Color.R, ball.Data.Color.G, ball.Data.Color.B))
            };

            Canvas.SetLeft(shape, ball.Data.X);
            Canvas.SetTop(shape, ball.Data.Y);
            shape.Tag = ball;
            MainCanvas.Children.Add(shape);
        }

        private void StartMoveTimer()
        {
            moveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20) };
            moveTimer.Tick += MoveBalls;
            moveTimer.Start();
        }

        private void MoveBalls(object sender, EventArgs e)
        {
            foreach (UIElement element in MainCanvas.Children)
            {
                if (element is Ellipse shape && shape.Tag is BallLogic ball)
                {
                    ball.Move((int)MainCanvas.ActualWidth, (int)MainCanvas.ActualHeight);
                    Canvas.SetLeft(shape, ball.Data.X);
                    Canvas.SetTop(shape, ball.Data.Y);
                }
            }

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    if (balls[i].CheckCollision(balls[j]))
                    {
                        balls[i].ResolveCollision(balls[j]);
                    }
                }
            }
        }
    }
}
