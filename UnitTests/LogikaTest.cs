using NUnit.Framework;
using Logika;
using Dane;
using NUnit.Framework.Legacy;

namespace UnitTests
{
    public class TestShape : IShape
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public double MovX { get; set; }
        public double MovY { get; set; }
    }

    [TestFixture]
    public class BallLogicTests
    {
        private IShape shape;
        private ILogic logic;

        [SetUp]
        public void Setup()
        {
            shape = new TestShape
            {
                X = 10,
                Y = 10,
                Radius = 20,
                MovX = 2,
                MovY = 3
            };

            logic = new BallLogic(shape);
        }

        [Test]
        public void BallLogic_Move_UpdatesPositionCorrectly()
        {
            logic.SimulateMove(100, 100);

            ClassicAssert.AreEqual(12, shape.X);
            ClassicAssert.AreEqual(13, shape.Y);
        }

        [Test]
        public void BallLogic_Move_BouncesOffWalls()
        {
            shape.X = 95;
            shape.MovX = 10;

            logic.SimulateMove(100, 100);

            ClassicAssert.Less(shape.MovX, 0);
        }
    }
}
