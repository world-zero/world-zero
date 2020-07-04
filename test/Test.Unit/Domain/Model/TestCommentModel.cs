using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestCommentModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var c = new CommentModel();
            // Ignore the milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(c.DateCreated.ToString("MM:dd:yyyy HH:mm"), DateTime.UtcNow.ToString("MM:dd:yyyy HH:mm"));
            Assert.IsNull(c.Comment);
            Assert.IsNull(c.Praxis);
            Assert.IsNull(c.Character);
        }
    }
}