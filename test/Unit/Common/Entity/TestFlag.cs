using WorldZero.Common.Entity;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Entity
{
    [TestFixture]
    public class TestFlag
    {
        [Test]
        public void TestDefaultValues()
        {
            var f = new Flag();
            Assert.IsNull(f.Name);
            Assert.IsNull(f.Description);
        }

        [Test]
        public void TestName()
        {
            var f = new Flag();
            var expected = "Invalid.";
            f.Name = expected;
            Assert.AreEqual(expected, f.Name);
            f.Name = "New";
            Assert.AreEqual("New", f.Name);
            Assert.Throws<ArgumentException>(()=>f.Name = null);
            Assert.Throws<ArgumentException>(()=>f.Name = "");
            Assert.Throws<ArgumentException>(()=>f.Name = "   ");
            Assert.AreEqual("New", f.Name);
        }
    }
}