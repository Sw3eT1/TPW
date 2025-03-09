namespace spawnRandomObejcts
{
    public partial class MyForm : Form
    {
        private List<Circle> balls = new();

        private System.Windows.Forms.Timer spawnTimer;
        private System.Windows.Forms.Timer moveTimer;
        private Random rand = new();

        public MyForm()
        {
            InitializeComponent();
            spawnTimer = new System.Windows.Forms.Timer();
            moveTimer = new System.Windows.Forms.Timer();
            InitializeSpawnTimer(); 
        }

        private void InitializeSpawnTimer()
        {
            spawnTimer.Interval = 200; 
            spawnTimer.Tick += TimerEvent;
            spawnTimer.Start();
        }

        private void InitializeMoveTimer()
        {
            moveTimer.Interval = 16; 
            moveTimer.Tick += MoveBalls;
            moveTimer.Start();
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            if (balls.Count < 10)
            {
                MakeNewBall();
            }
            else
            {
                spawnTimer.Stop(); 
                InitializeMoveTimer(); 
            }
        }

        public void MakeNewBall()
        {
            Circle circle = new(rand, ClientSize.Width, ClientSize.Height);
            balls.Add(circle);
            Controls.Add(circle.PictureBox);
        }

        private void MoveBalls(object sender, EventArgs e)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Move(ClientSize.Width, ClientSize.Height);

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
