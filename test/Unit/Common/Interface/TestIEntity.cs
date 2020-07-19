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
        private int _id;
        private Name _name;

        [SetUp]
        public void CreateInstance()
        {
            this._id = 1;
            this._name = new Name("Pizza");
            this._e = new TestEntity()
            {
                Id = this._id
            };
        }

        [Test]
        public void TestId()
        {
            Assert.IsTrue(this._e.Id == this._id);
            this._e.Id = 0;
            Assert.Throws<ArgumentException>(()=>this._e.Id = -1);
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

    public class TestEntity : IEntity
    {
        public new T Eval<T>(ISingleValueObject<T> svo, T other)
        {
            return base.Eval(svo, other);
        }
    }
}