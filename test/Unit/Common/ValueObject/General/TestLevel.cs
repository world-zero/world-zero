using System;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.ValueObject.General
{
    [TestFixture]
    public class TestLevel
    {
        private Level _vo;
        private int _defaultVal;

        [SetUp]
        public void CreateInstance()
        {
            this._defaultVal = 5;
            this._vo = new Level(this._defaultVal);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._vo.Get == this._defaultVal);
            Assert.IsTrue(new Level(0).Get == 0);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new Level(-1));
        }

        [Test]
        public void TestCalculateLevel()
        {
            var points = new PointTotal(0);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(0));
            points = new PointTotal(10-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(0));

            points = new PointTotal(10);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(1));
            points = new PointTotal(70-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(1));

            points = new PointTotal(70);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(2));
            points = new PointTotal(170-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(2));

            points = new PointTotal(170);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(3));
            points = new PointTotal(330-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(3));

            points = new PointTotal(330);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(4));
            points = new PointTotal(610-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(4));

            points = new PointTotal(610);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(5));
            points = new PointTotal(1090-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(5));

            points = new PointTotal(1090);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(6));
            points = new PointTotal(1840-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(6));

            points = new PointTotal(1840);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(7));
            points = new PointTotal(3040-1);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(7));

            points = new PointTotal(3040);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(8));
            points = new PointTotal(3041);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(8));
            points = new PointTotal(1000000);
            Assert.AreEqual(Level.CalculateLevel(points), new Level(8));
        }
    }
}
