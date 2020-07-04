using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestLocationModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var l = new LocationModel();
            Assert.IsNull(l.City);
            Assert.IsNull(l.State);
            Assert.IsNull(l.Country);
            Assert.IsNull(l.Zip);
        }
    }
}