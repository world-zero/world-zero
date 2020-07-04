using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestFactionModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var f = new FactionModel();
            // Ignore the milliseconds/seconds so this test doesn't fail.
            Assert.AreEqual(f.DateFounded.ToString("MM:dd:yyyy HH:mm"), DateTime.UtcNow.ToString("MM:dd:yyyy HH:mm"));
            Assert.IsNull(f.FactionName);
            Assert.IsNull(f.Description);
            Assert.IsNull(f.AbilityName);
            Assert.IsNull(f.AbilityDesc);
        }
    }
}