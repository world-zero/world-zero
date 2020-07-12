using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestTag
    {
        private Tag _t;
        private string _tagName;

        [SetUp]
        public void Setup()
        {
            this._tagName = "boop";

            this._t = new Tag();
            this._t.TagName = this._tagName;
        }

        [Test]
        public void TestDefaultValues()
        {
            var s = new Tag();
            Assert.IsNull(s.TagName);
            Assert.IsNull(s.Description);
        }

        [Test]
        public void TestTagName()
        {
            Assert.AreEqual(this._tagName, this._t.TagName);
            this._t.TagName = "Test";
            Assert.AreEqual("Test", this._t.TagName);
            Assert.Throws<ArgumentException>(()=>this._t.TagName = null);
            Assert.Throws<ArgumentException>(()=>this._t.TagName = "");
            Assert.Throws<ArgumentException>(()=>this._t.TagName = "     ");
            Assert.AreEqual("Test", this._t.TagName);
        }
    }
}