using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
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