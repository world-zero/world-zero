using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface
{
    [TestFixture]
    public class TestINamedEntity
    {
        private TestNameEntity _e;
        private Name _id;

        [SetUp]
        public void CreateInstance()
        {
            this._id = new Name("Pizza");
            this._e = new TestNameEntity()
            {
                Id = this._id.Get
            };
        }

        [Test]
        public void TestId()
        {
            Assert.AreEqual(this._id.Get, this._e.Id);
            this._e.Id = "Test";
            Assert.AreEqual("Test", this._e.Id);
            Assert.Throws<ArgumentException>(()=>this._e.Id = null);
            Assert.Throws<ArgumentException>(()=>this._e.Id = "");
            Assert.Throws<ArgumentException>(()=>this._e.Id = "     ");
            Assert.AreEqual("Test", this._e.Id);
        }
    }

    public class TestNameEntity : INamedEntity { }
}