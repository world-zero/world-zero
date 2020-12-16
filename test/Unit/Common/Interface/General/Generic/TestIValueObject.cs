using System.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.General.Generic
{
    [TestFixture]
    public class TestIValueObject
    {
        private ValObj _vo;
        private int _defaultVal = 13;

        [SetUp]
        public void CreateInstance()
        {
            this._vo = new ValObj(this._defaultVal);
        }

        [Test]
        public void TestOperators()
        {
            var happy = new ValObj(this._defaultVal);
            var sad = new ValObj(1);
            Assert.IsTrue(this._vo == happy, "The == operator is not overridden.");
            Assert.IsTrue(this._vo != sad, "The != operator is not overridden.");
            Assert.IsTrue(this._vo.Equals(happy));
            Assert.IsFalse(this._vo.Equals(sad));
            Assert.IsFalse(this._vo.Equals(9));
            Assert.IsFalse(this._vo.Equals(31));
        }
    }

    public class ValObj : IValueObject
    {
        public int Get { get; private set; }
        public ValObj(int x)
        {
            this.Get = x;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}