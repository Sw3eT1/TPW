using Dane;

namespace Logika
{

    public class BallLogic : ILogic
    {
        public IShape Data { get; set; }

        public BallLogic(int width, int height)
        {
            Data = BallData.CreateRandomShape(width, height);
        }

        public BallLogic(IShape shape)
        {
            Data = shape;
        }

        public void SimulateMove(int maxWidth, int maxHeight)
        {
            Data.Start(maxWidth, maxHeight);
        }

        public bool CheckCollision(ILogic obj)
        {
            double dx = (Data.X + Data.Radius / 2) - (obj.Data.X + obj.Data.Radius / 2);
            double dy = (Data.Y + Data.Radius / 2) - (obj.Data.Y + obj.Data.Radius / 2);
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double minDistance = (Data.Radius / 2 + obj.Data.Radius / 2) * 1.1;
            return distance <= minDistance;
        }

        public void ResolveCollision(ILogic obj)
        {
            double dx = (Data.X + Data.Radius / 2) - (obj.Data.X + obj.Data.Radius / 2);
            double dy = (Data.Y + Data.Radius / 2) - (obj.Data.Y + obj.Data.Radius / 2);
            double distance = Math.Sqrt(dx * dx + dy * dy);

            double minDistance = (Data.Radius / 2 + obj.Data.Radius / 2) * 1.1;
            if (distance < minDistance && distance != 0)
            {
                double overlap = minDistance - distance;
                double pushX = dx / distance * (overlap / 2);
                double pushY = dy / distance * (overlap / 2);

                Data.X += pushX;
                Data.Y += pushY;
                obj.Data.X -= pushX;
                obj.Data.Y -= pushY;

                double nx = dx / distance;
                double ny = dy / distance;

                double dvx = Data.MovX - obj.Data.MovX;
                double dvy = Data.MovY - obj.Data.MovY;

                double dot = dvx * nx + dvy * ny;

                if (dot > 0) return;

                double m1 = Data.Mass;
                double m2 = obj.Data.Mass;

                double coefficient = 1;

                double impulse = (2 * dot) / (m1 + m2);

                Data.MovX -= impulse * m2 * nx;
                Data.MovY -= impulse * m2 * ny;

                obj.Data.MovX += impulse * m1 * nx;
                obj.Data.MovY += impulse * m1 * ny;
            }
        }

    }
}
