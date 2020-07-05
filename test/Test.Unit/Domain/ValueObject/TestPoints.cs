using System;
using WorldZero.Domain.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.ValueObject
{
    [TestFixture]
    public class TestPoints
    {
        private Points _vo;
        private int _defaultVal;

        [SetUp]
        public void CreateInstance()
        {
            this._defaultVal = 5;
            this._vo = new Points(this._defaultVal);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._vo.Get == this._defaultVal);
            Assert.IsTrue(new Points(0).Get == 0);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new Points(-1));
        }
    }
}
