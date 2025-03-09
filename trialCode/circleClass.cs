using System.Drawing.Drawing2D;

class Circle
{
    public PictureBox PictureBox { get; private set; }
    public float MovmentX { get; private set; }
    public float MovmentY { get; private set; }

    public Circle(Random random, int maxWidth, int maxHeight)
    {
        int radius = random.Next(20, 50);
        PictureBox = new PictureBox
        {
            Width = radius,
            Height = radius,
            BackColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)),
            Location = new Point(random.Next(10, maxWidth - radius), random.Next(10, maxHeight - radius))
        };

        
        GraphicsPath path = new();
        path.AddEllipse(0, 0, radius, radius);
        PictureBox.Region = new Region(path);

       
        MovmentX = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1);
        MovmentY = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1);
    }

    public void Move(int maxWidth, int maxHeight)
    {
        int newX = PictureBox.Left + (int)MovmentX;
        int newY = PictureBox.Top + (int)MovmentY;

        
        if (newX <= 0 || newX + PictureBox.Width >= maxWidth) MovmentX = -MovmentX;
        if (newY <= 0 || newY + PictureBox.Height >= maxHeight) MovmentY = -MovmentY;

        
        PictureBox.Left += (int)MovmentX;
        PictureBox.Top += (int)MovmentY;
    }

    public bool CheckCollision(Circle other)
{
    float dx = (PictureBox.Left + PictureBox.Width / 2) - (other.PictureBox.Left + other.PictureBox.Width / 2);
    float dy = (PictureBox.Top + PictureBox.Height / 2) - (other.PictureBox.Top + other.PictureBox.Height / 2);
    float distance = (float)Math.Sqrt(dx * dx + dy * dy);

    float minDistance = (PictureBox.Width / 2 + other.PictureBox.Width / 2) * 1.1f; // Zwiększenie marginesu o 10%

    return distance <= minDistance;
}

public void ResolveCollision(Circle other)
{
    float dx = (PictureBox.Left + PictureBox.Width / 2) - (other.PictureBox.Left + other.PictureBox.Width / 2);
    float dy = (PictureBox.Top + PictureBox.Height / 2) - (other.PictureBox.Top + other.PictureBox.Height / 2);
    float distance = (float)Math.Sqrt(dx * dx + dy * dy);
    
    float minDistance = (PictureBox.Width / 2 + other.PictureBox.Width / 2) * 1.1f;
    
    if (distance < minDistance)
    {
        float overlap = minDistance - distance;
        float pushX = dx / distance * (overlap / 2);
        float pushY = dy / distance * (overlap / 2);
        
        PictureBox.Left += (int)pushX;
        PictureBox.Top += (int)pushY;
        other.PictureBox.Left -= (int)pushX;
        other.PictureBox.Top -= (int)pushY;
    }

    float tempDx = MovmentX;
    float tempDy = MovmentY;

    MovmentX = other.MovmentX;
    MovmentY = other.MovmentY;

    other.MovmentX = tempDx;
    other.MovmentY = tempDy;
}

}
