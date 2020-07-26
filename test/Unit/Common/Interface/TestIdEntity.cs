using System;
using WorldZero.Common.Interface;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface
{
    [TestFixture]
    public class TestIIdEntity
    {
        private TestIdEntity _e;
        private int _id;

        [SetUp]
        public void CreateInstance()
        {
            this._id = 1;
            this._e = new TestIdEntity()
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
    }

    public class TestIdEntity : IIdEntity { }
}