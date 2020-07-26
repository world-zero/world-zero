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
            Assert.IsNull(f.Id);
            Assert.IsNull(f.Description);
        }

        [Test]
        public void TestId()
        {
            var f = new Flag();
            var expected = "Invalid.";
            f.Id = expected;
            Assert.AreEqual(expected, f.Id);
            f.Id = "New";
            Assert.AreEqual("New", f.Id);
            Assert.Throws<ArgumentException>(()=>f.Id = null);
            Assert.Throws<ArgumentException>(()=>f.Id = "");
            Assert.Throws<ArgumentException>(()=>f.Id = "   ");
            Assert.AreEqual("New", f.Id);
        }
    }
}