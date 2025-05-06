using System.Drawing;

namespace Dane
{
    public class BallData : IShape
    {
            private readonly object positionLock = new object();

            private double x;
            private double y;

            public double X
            {
                get
                {
                    lock (positionLock)
                    {
                        return x;
                    }
                }
                set
                {
                    lock (positionLock)
                    {
                        this.x = value;
                    }
                }
            }

            public double Y
            {
                get
                {
                    lock (positionLock)
                    {
                        return y;
                    }
                }
                set
                {
                    lock (positionLock)
                    {
                        this.y = value;
                    }
                }
            }

            public double MovX { get; set; }
            public double MovY { get; set; }
            public double Radius { get; set; }
            public Color Color { get; set; }
            public double Mass { get; set; } 

            public static IShape CreateRandomShape(int maxWidth, int maxHeight)
            {
                Random localRandom = new Random();
                double minMass = 1;
                double maxMass = 5;
                double mass = minMass + (maxMass - minMass) * localRandom.NextDouble();

                double minRadius = 15;
                double maxRadius = 40;
                double radius = minRadius + (maxRadius - minRadius) * ((mass - minMass) / (maxMass - minMass));

                double baseSpeed = 8;
                double speedFactor = 1.0 / mass; 

                double initialMovX = baseSpeed * localRandom.NextDouble() * speedFactor * (localRandom.Next(2) == 0 ? 1 : -1);
                double initialMovY = baseSpeed * localRandom.NextDouble() * speedFactor * (localRandom.Next(2) == 0 ? 1 : -1);

                double clampedRadius = Math.Min(radius, Math.Min(maxWidth / 4.0, maxHeight / 4.0));

                return new BallData
                {
                    Radius = clampedRadius,
                    X = localRandom.Next(10, maxWidth - (int)clampedRadius),
                    Y = localRandom.Next(10, maxHeight - (int)clampedRadius),
                    MovX = initialMovX,
                    MovY = initialMovY,
                    Color = Color.FromArgb(localRandom.Next(256), localRandom.Next(256), localRandom.Next(256)),
                    Mass = mass
                };
            }

            public void Move(int maxWidth, int maxHeight)
            {
                lock (positionLock)
                {
                    x += MovX;
                    y += MovY;

                    if (x <= 0 || x + Radius >= maxWidth) MovX = -MovX;
                    if (y <= 0 || y + Radius >= maxHeight) MovY = -MovY;
                }
            }
        }
    }