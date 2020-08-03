using System;
using WorldZero.Common.Interface.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface
{
    [TestFixture]
    public class TestIIdIdEntity
    {
        private TestIdIdEntity _e;
        private int _id0;
        private int _id1;

        [SetUp]
        public void CreateInstance()
        {
            this._id0 = 1;
            this._id1 = 2;
            this._e = new TestIdIdEntity()
            {
                LeftId = this._id0,
                RightId = this._id1,
            };
        }

        [Test]
        public void TestLeftId()
        {
            Assert.IsTrue(this._e.LeftId == this._id0);
            this._e.LeftId = 0;
            Assert.IsTrue(this._e.LeftId == 0);
            Assert.Throws<ArgumentException>(()=>this._e.LeftId = -1);
            Assert.IsTrue(this._e.LeftId == 0);
        }

        [Test]
        public void TestRightId()
        {
            Assert.IsTrue(this._e.RightId == this._id1);
            this._e.RightId = 0;
            Assert.IsTrue(this._e.RightId == 0);
            Assert.Throws<ArgumentException>(()=>this._e.RightId = -1);
            Assert.IsTrue(this._e.RightId == 0);
        }
    }

    public class TestIdIdEntity : IIdIdEntityMap { }
}