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
        public event Action<double, double> PositionChanged;
        public void Start();
        public void Stop();
    }
}
