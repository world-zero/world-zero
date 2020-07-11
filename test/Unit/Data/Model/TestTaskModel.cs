using System;
using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestTaskModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var t = new TaskModel();
            Assert.IsNull(t.Summary);
            Assert.IsNull(t.MinLevel);
            Assert.IsNull(t.Faction);
            Assert.IsNull(t.Status);
        }
    }
}