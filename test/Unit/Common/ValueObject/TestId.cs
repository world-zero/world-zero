using System;
using WorldZero.Common.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.ValueObject
{
    [TestFixture]
    public class TestId
    {
        private Id _vo;
        private int _defaultVal = 13;

        [SetUp]
        public void CreateInstance()
        {
            this._vo = new Id(this._defaultVal);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._vo.Get == this._defaultVal);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new Id(-1));
            new Id(0);
        }
    }
}