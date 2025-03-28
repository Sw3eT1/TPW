using System.Drawing;
using NUnit.Framework;
using Logika;
using NUnit.Framework.Legacy;

namespace UnitTests
{
    [TestFixture]
    public class LogikaTest
    {
        private Random random;
        private int maxWidth;
        private int maxHeight;

        [SetUp]
        public void Setup()
        {
            random = new Random(1);
            maxWidth = 100;
            maxHeight = 100;
        }

        [Test]
        public void BallLogic_ConstructorTests()
        {
            BallLogic ballLogic = new BallLogic(random, maxWidth, maxHeight);

            ClassicAssert.IsNotNull(ballLogic.Data);
            ClassicAssert.IsTrue(ballLogic.Data.X >= 10 && ballLogic.Data.X <= maxWidth);
            ClassicAssert.IsTrue(ballLogic.Data.Y >= 10 && ballLogic.Data.Y <= maxHeight);
            ClassicAssert.IsTrue(ballLogic.Data.Radius >= 20 && ballLogic.Data.Radius <= 50);
            ClassicAssert.IsTrue(ballLogic.Data.MovX >= -5 && ballLogic.Data.MovX <= 5);
            ClassicAssert.IsTrue(ballLogic.Data.MovY >= -5 && ballLogic.Data.MovY <= 5);
            ClassicAssert.IsNotNull(ballLogic.Data.Color);
        }

        [Test]
        public void BallLogic2_ConstructorTests()
        {
            BallLogic ballLogic = new BallLogic(random, maxWidth, maxHeight);

            ClassicAssert.IsNull(ballLogic.Data);
            ClassicAssert.IsTrue(ballLogic.Data.X >= 10 && ballLogic.Data.X <= maxWidth);
            ClassicAssert.IsTrue(ballLogic.Data.Y >= 10 && ballLogic.Data.Y <= maxHeight);
            ClassicAssert.IsTrue(ballLogic.Data.Radius >= 20 && ballLogic.Data.Radius <= 50);
            ClassicAssert.IsTrue(ballLogic.Data.MovX >= -5 && ballLogic.Data.MovX <= 5);
            ClassicAssert.IsTrue(ballLogic.Data.MovY >= -5 && ballLogic.Data.MovY <= 5);
            ClassicAssert.IsNotNull(ballLogic.Data.Color);
        }
    }
}
