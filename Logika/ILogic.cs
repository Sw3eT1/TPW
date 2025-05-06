using Dane;

namespace Logika
{
    public interface ILogic
    {
        IShape Data { get; }
        void SimulateMove(int maxWidth, int maxHeight);
        bool CheckCollision(ILogic other);
        void ResolveCollision(ILogic other);
    }
}