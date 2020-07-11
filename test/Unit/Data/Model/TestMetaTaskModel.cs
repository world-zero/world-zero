using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestMetaTaskModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var mt = new MetaTaskModel();
            Assert.IsNull(mt.MetaTaskName);
            Assert.IsNull(mt.Description);
            Assert.AreEqual(mt.Bonus, 0);
            Assert.AreEqual(mt.IsFlatBonus, true);
            Assert.IsNull(mt.Status);
        }
    }
}