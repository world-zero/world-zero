using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestComment
    {
        [Test]
        public void TestDefaultValues()
        {
            var c = new Comment();
            // Ignore the milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(c.DateCreated.ToString("MM:dd:yyyy HH:mm"), DateTime.UtcNow.ToString("MM:dd:yyyy HH:mm"));
            Assert.IsNull(c.Value);
            Assert.IsNull(c.Praxis);
            Assert.IsNull(c.Character);
        }

        [Test]
        public void TestDateFounded()
        {
            var c = new Comment();
            var d = DateTime.UtcNow;
            c.DateCreated = d;
            Assert.AreEqual(c.DateCreated, d);
            Assert.Throws<ArgumentException>(()=>c.DateCreated = new DateTime(3000, 5, 1, 8, 30, 52));
            Assert.AreEqual(c.DateCreated, d);
        }
    }
}