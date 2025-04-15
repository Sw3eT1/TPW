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


        public void Move(int maxWidth, int maxHeight)
        {
            Data.X += Data.MovX;
            Data.Y += Data.MovY;

            if (Data.X <= 0 || Data.X + Data.Radius >= maxWidth) Data.MovX = -Data.MovX;
            if (Data.Y <= 0 || Data.Y + Data.Radius >= maxHeight) Data.MovY = -Data.MovY;
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
