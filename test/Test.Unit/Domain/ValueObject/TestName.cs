using System;
using WorldZero.Domain.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.ValueObject
{
    [TestFixture]
    public class TestName
    {
        private Name _vo;
        private string _defaultVal = "Sawyer";

        [SetUp]
        public void CreateInstance()
        {
            this._vo = new Name(this._defaultVal);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._vo.Get == this._defaultVal);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new Name(null));
            Assert.Throws<ArgumentException>(()=>new Name(""));
            Assert.Throws<ArgumentException>(()=>new Name("             "));
        }
    }
}