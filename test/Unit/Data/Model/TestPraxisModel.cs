using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestPraxisModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var p = new PraxisModel();
            Assert.IsNull(p.Task);
            Assert.IsFalse(p.IsDueling);
            Assert.IsNull(p.Status);
        }
    }
}