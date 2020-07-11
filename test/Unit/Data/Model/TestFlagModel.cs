using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
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