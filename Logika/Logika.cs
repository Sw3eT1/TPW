using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dane;

namespace Logika
{

    public class BallLogic
    {
        public BallData Data { get; private set; }

        public BallLogic(Random random, int maxWidth, int maxHeight)
        {
            double radius = random.Next(20, 50);
            Data = new BallData
            {
                X = random.Next(10, maxWidth - (int)radius),
                Y = random.Next(10, maxHeight - (int)radius),
                Radius = radius,
                MovX = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                MovY = random.Next(2, 5) * (random.Next(2) == 0 ? 1 : -1),
                Color = Color.FromArgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))
            };
        }

        public void Move(int maxWidth, int maxHeight)
        {
            Data.X += Data.MovX;
            Data.Y += Data.MovY;

            if (Data.X <= 0 || Data.X + Data.Radius >= maxWidth) Data.MovX = -Data.MovX;
            if (Data.Y <= 0 || Data.Y + Data.Radius >= maxHeight) Data.MovY = -Data.MovY;
        }

        public bool CheckCollision(BallLogic obj)
        {
            double dx = (Data.X + Data.Radius / 2) - (obj.Data.X + obj.Data.Radius / 2);
            double dy = (Data.Y + Data.Radius / 2) - (obj.Data.Y + obj.Data.Radius / 2);
            double distance = Math.Sqrt(dx * dx + dy * dy);
            double minDistance = (Data.Radius / 2 + obj.Data.Radius / 2) * 1.1;
            return distance <= minDistance;
        }

        public void ResolveCollision(BallLogic obj)
        {
            double dx = (Data.X + Data.Radius / 2) - (obj.Data.X + obj.Data.Radius / 2);
            double dy = (Data.Y + Data.Radius / 2) - (obj.Data.Y + obj.Data.Radius / 2);
            double distance = Math.Sqrt(dx * dx + dy * dy);

            double minDistance = (Data.Radius / 2 + obj.Data.Radius / 2) * 1.1;
            if (distance < minDistance)
            {
                double overlap = minDistance - distance;
                double pushX = dx / distance * (overlap / 2);
                double pushY = dy / distance * (overlap / 2);

                Data.X += pushX;
                Data.Y += pushY;
                obj.Data.X -= pushX;
                obj.Data.Y -= pushY;
            }

            double tempMovX = Data.MovX;
            double tempMovY = Data.MovY;
            Data.MovX = obj.Data.MovX;
            Data.MovY = obj.Data.MovY;
            obj.Data.MovX = tempMovX;
            obj.Data.MovY = tempMovY;
        }
    }
}
