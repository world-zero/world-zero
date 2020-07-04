using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestFlagModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var f = new FlagModel();
            Assert.IsNull(f.FlagName);
            Assert.IsNull(f.Description);
        }
    }
}