using System.Drawing;

namespace Dane
{
    public class BallData : IShape
    {
        private static readonly Random random = new Random();

        public double X { get; set; }
        public double Y { get; set; }
        public double MovX { get; set; }
        public double MovY { get; set; }
        public double Radius { get; set; }
        public Color Color { get; set; }

        // ⬇⬇⬇ TUTAJ wstaw tę metodę ⬇⬇⬇
        public static IShape CreateRandomShape(int maxWidth, int maxHeight)
        {
            double radius = random.Next(20, 50);
            return new BallData
            {
                Radius = radius,
                X = random.Next(10, maxWidth - (int)radius),
                Y = random.Next(10, maxHeight - (int)radius),
                MovX = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                MovY = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))
            };
        }
    }
}
