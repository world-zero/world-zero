using System;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.ValueObject.General
{
    [TestFixture]
    public class TestPointTotal
    {
        private PointTotal _vo;
        private int _defaultVal;

        [SetUp]
        public void CreateInstance()
        {
            this._defaultVal = 5;
            this._vo = new PointTotal(this._defaultVal);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._vo.Get == this._defaultVal);
            Assert.IsTrue(new PointTotal(0).Get == 0);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new PointTotal(-1));
        }

        [Test]
        public void TestApplyPenaltyFlatHappy()
        {
            Assert.AreEqual(
                new PointTotal(125),
                PointTotal.ApplyBonus(new PointTotal(75), 50, true)
            );
        }

        [Test]
        public void TestApplyBonusFlatSad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                PointTotal.ApplyBonus(null, 50, true));

            Assert.Throws<ArgumentException>(()=>PointTotal.ApplyBonus(
                new PointTotal(324),
                Convert.ToDouble(int.MaxValue)+10000,
                true));
        }

        [Test]
        public void TestApplyBonusPercentHappy()
        {
            Assert.AreEqual(
                new PointTotal(110),
                PointTotal.ApplyBonus(new PointTotal(100), 0.1, false)
            );
            Assert.AreEqual(
                new PointTotal(110),
                PointTotal.ApplyBonus(new PointTotal(100), -0.1, false)
            );
        }

        [Test]
        public void TestApplyPenaltyFlatSad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                PointTotal.ApplyPenalty(null, 40));

            Assert.AreEqual(
                new PointTotal(0),
                PointTotal.ApplyPenalty(new PointTotal(10), 1000));

            Assert.Throws<ArgumentException>(()=>PointTotal.ApplyPenalty(
                new PointTotal(324),
                Convert.ToDouble(int.MaxValue)+10000,
                true));
        }

        [Test]
        public void TestApplyPenaltyPercentHappy()
        {
            Assert.AreEqual(
                new PointTotal(90),
                PointTotal.ApplyPenalty(new PointTotal(100), 0.1, false)
            );
            Assert.AreEqual(
                new PointTotal(90),
                PointTotal.ApplyPenalty(new PointTotal(100), -0.1, false)
            );
        }

        [Test]
        public void TestApplyPenaltyPercentSad()
        {
            Assert.AreEqual(
                new PointTotal(0),
                PointTotal.ApplyPenalty(new PointTotal(10), 1.1, false)
            );
        }
    }
}
