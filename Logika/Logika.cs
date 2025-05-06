using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Data.Move(maxWidth, maxHeight);
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

                // Jednostkowy wektor kolizji
                double nx = dx / distance;
                double ny = dy / distance;

                // Składowe prędkości w kierunku kolizji
                double dvx = Data.MovX - obj.Data.MovX;
                double dvy = Data.MovY - obj.Data.MovY;

                double dot = dvx * nx + dvy * ny;

                if (dot > 0) return; // Kule już się oddalają

                double m1 = Data.Mass;
                double m2 = obj.Data.Mass;

                // Współczynnik sprężystości 1 (sprężysta kolizja)
                double coefficient = 1;

                // Oblicz impuls
                double impulse = (2 * dot) / (m1 + m2);

                // Zastosuj zmianę prędkości na podstawie masy i impulsu
                Data.MovX -= impulse * m2 * nx;
                Data.MovY -= impulse * m2 * ny;

                obj.Data.MovX += impulse * m1 * nx;
                obj.Data.MovY += impulse * m1 * ny;
            }
        }

    }
}
