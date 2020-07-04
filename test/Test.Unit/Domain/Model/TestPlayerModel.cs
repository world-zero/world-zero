using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestPlayerModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var p = new PlayerModel();
            Assert.IsNull(p.Username);
            Assert.AreEqual(p.IsBlocked, false);
        }
    }
}