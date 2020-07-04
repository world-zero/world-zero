using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestEraModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var e = new EraModel();
            // Ignore the milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(e.StartDate.ToString("MM:dd:yyyy HH:mm"), DateTime.UtcNow.ToString("MM:dd:yyyy HH:mm"));
            Assert.IsNull(e.EraName);
            Assert.IsNull(e.EndDate);
        }
    }
}