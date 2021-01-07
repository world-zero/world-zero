using System;
using WorldZero.Common.Interface.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.General.Generic
{
    [TestFixture]
    public class TestSingleValueObject
    {
        private AnId _vo;
        private int _defaultVal = 13;

        [SetUp]
        public void CreateInstance()
        {
            this._vo = new AnId(this._defaultVal);
        }

        [Test]
        public void TestConstructorHappy()
        {
            Assert.IsTrue(this._vo.Get == this._defaultVal);
            new AnId(0);
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentException>(()=>new AnId(-1));
        }
    }

    public class AnId : ABCSingleValueObject<int>
    {
        public override int Get
        {
            get { return this._val; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("An ID cannot be negative.");
                this._val = value;
            }
        }

        public AnId(int id)
            : base(id)
        {
        }
    }
}