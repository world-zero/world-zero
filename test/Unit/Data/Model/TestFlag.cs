using WorldZero.Common.Model;
using System;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestFlag
    {
        [Test]
        public void TestDefaultValues()
        {
            var f = new Flag();
            Assert.IsNull(f.FlagName);
            Assert.IsNull(f.Description);
        }

        [Test]
        public void TestName()
        {
            var f = new Flag();
            var expected = "Invalid.";
            f.FlagName = expected;
            Assert.AreEqual(expected, f.FlagName);
            f.FlagName = "New";
            Assert.AreEqual("New", f.FlagName);
            Assert.Throws<ArgumentException>(()=>f.FlagName = null);
            Assert.Throws<ArgumentException>(()=>f.FlagName = "");
            Assert.Throws<ArgumentException>(()=>f.FlagName = "   ");
            Assert.AreEqual("New", f.FlagName);
        }
    }
}