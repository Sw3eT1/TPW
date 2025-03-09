using System.Drawing.Drawing2D;

namespace spawnRandomObejcts
{
    public partial class MyForm : Form
    {
        private List<PictureBox> balls = new();
        private System.Windows.Forms.Timer spawnTimer;
        private int spawnRate = 20;
        private Random rand = new();

        public MyForm()
        {
            InitializeComponent();
            InitializeTimer(); 
        }

        private void InitializeTimer()
        {
            spawnTimer = new System.Windows.Forms.Timer();
            spawnTimer.Interval = 500; 
            spawnTimer.Tick += TimerEvent;
            spawnTimer.Start();
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            if (spawnRate > 0)
            {
                MakeNewBall();
                spawnRate--;
            }
            else
            {
                spawnTimer.Stop();
            }
        }

        public void MakeNewBall()
        {
            Circle circle1 = new();
            PictureBox newBall = new();
            GraphicsPath path = new();

            newBall.Height = circle1.Radius;
            newBall.Width = circle1.Radius;
            newBall.BackColor = circle1.Color;

            circle1.X = rand.Next(10, ClientSize.Width - newBall.Width);
            circle1.Y = rand.Next(10, ClientSize.Height - newBall.Height);

            newBall.Location = new Point(circle1.X, circle1.Y);
            path.AddEllipse(0, 0, newBall.Width, newBall.Height);
            newBall.Region = new Region(path);

            balls.Add(newBall);
            Controls.Add(newBall);
        }
    }
}
