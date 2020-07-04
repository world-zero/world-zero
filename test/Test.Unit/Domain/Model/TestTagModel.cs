using System;
using System.Reflection;
using WorldZero.Domain.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Domain.Model
{
    [TestFixture]
    public class TestTagModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var t = new TagModel();
            Assert.IsNull(t.TagName);
            Assert.IsNull(t.Description);
        }
    }
}