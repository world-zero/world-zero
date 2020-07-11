using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
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