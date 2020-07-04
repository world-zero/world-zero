using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestStatusModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var s = new StatusModel();
            Assert.IsNull(s.StatusName);
            Assert.IsNull(s.Description);
        }
    }
}