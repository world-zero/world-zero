using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
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