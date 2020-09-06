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
    }
}
