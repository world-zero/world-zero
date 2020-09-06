using System;
using WorldZero.Common.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.ValueObject
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

        [Test]
        public void TestMaxLength()
        {
            Assert.IsTrue(Name.MaxLength == 50, "Name's MaxLength has been changed, these tests are going to fail - update these tests.");
            new Name("12345678901234567890123451234567890123456789012345");
            Assert.Throws<ArgumentException>(()=>new Name("123456789012345678901234512345678901234567890123456"));
        }
    }
}