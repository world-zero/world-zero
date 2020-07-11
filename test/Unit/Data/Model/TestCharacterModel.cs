using WorldZero.Data.Model;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Model
{
    [TestFixture]
    public class TestCharacterModel
    {
        [Test]
        public void TestDefaultValues()
        {
            var c = new CharacterModel();
            Assert.IsNull(c.Displayname);
            Assert.IsFalse(c.HasBio);
            Assert.IsFalse(c.HasProfilePic);
            Assert.AreEqual(c.EraPoints, 0);
            Assert.AreEqual(c.TotalPoints, 0);
            Assert.AreEqual(c.EraLevel, 0);
            Assert.AreEqual(c.TotalLevel, 0);
            Assert.AreEqual(c.VotePointsLeft, 100);
            Assert.IsNull(c.Faction);
            Assert.IsNull(c.Location);
        }
    }
}