﻿using System.Drawing;
using NUnit.Framework;
using Dane;
using NUnit.Framework.Legacy;

namespace UnitTests
{
    [TestFixture]
    public class BallDataTests
    {
        [Test]
        public void BallData_SetAndGetTests()
        {
            IShape ballData = new BallData();
            double x = 10;
            double y = 20;
            double movX = 4;
            double movY = -5;

            ballData.X = x;
            ballData.Y = y;
            ballData.MovX = movX;
            ballData.MovY = movY;

            Assert.That(ballData.X, Is.EqualTo(x));
            Assert.That(ballData.Y, Is.EqualTo(y));
            Assert.That(movX, Is.EqualTo(ballData.MovX));
            Assert.That(movY, Is.EqualTo(ballData.MovY));
        }
    }
}
