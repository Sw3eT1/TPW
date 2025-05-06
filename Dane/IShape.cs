using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public interface IShape
    {
        double X { get; set; }
        double Y { get; set; }
        double MovX { get; set; }
        double MovY { get; set; }
        double Radius { get; set; }
        double Mass { get; set; }
        public void Move(int maxWidth, int maxHeight)
        {
        }
    }
}
