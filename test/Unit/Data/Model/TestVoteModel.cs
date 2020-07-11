using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestVoteModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var v = new VoteModel();
            Assert.IsNull(v.Praxis);
            Assert.IsNull(v.Character);
        }
    }
}