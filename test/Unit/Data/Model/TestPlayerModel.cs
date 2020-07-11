using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
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