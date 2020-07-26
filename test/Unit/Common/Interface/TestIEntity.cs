using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface
{
    [TestFixture]
    public class TestIEntity
    {
        private TestEntity _e;
        private Name _name;

        [SetUp]
        public void CreateInstance()
        {
            this._name = new Name("Pizza");
            this._e = new TestEntity()
            {
                Id = this._name.Get
            };
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._name.Get, this._e.Id);
            this._e.Id = "Test";
            Assert.AreEqual("Test", this._e.Id);
            Assert.Throws<ArgumentException>(()=>this._e.Id = null);
            Assert.Throws<ArgumentException>(()=>this._e.Id = "");
            Assert.Throws<ArgumentException>(()=>this._e.Id = "     ");
            Assert.AreEqual("Test", this._e.Id);
        }

        [Test]
        public void TestEval()
        {
            string expected = this._name.Get;
            string result = this._e.Eval<string>((ISingleValueObject<string>) this._name, "Pie");
            Assert.AreEqual(expected, result);
            result = this._e.Eval<string>((ISingleValueObject<string>) null, "Pie");
            Assert.AreEqual("Pie", result);
        }
    }

    public class TestEntity : IEntity<string>
    {
        public override string Id
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._id,
                    null);
            }
            set { this._id = new Name(value); }
        }

        public new T Eval<T>(ISingleValueObject<T> svo, T other)
        {
            return base.Eval(svo, other);
        }
    }
}