using System;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.ValueObject.General
{
    [TestFixture]
    public class TestPastDate
    {
        private PastDate _vo;
        private DateTime _defaultVal;

        [SetUp]
        public void CreateInstance()
        {
            this._defaultVal = DateTime.UtcNow;
            this._vo = new PastDate(this._defaultVal);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._vo.Get == this._defaultVal);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new PastDate(new DateTime(3000, 5, 1, 8, 30, 52)));
        }
    }
}