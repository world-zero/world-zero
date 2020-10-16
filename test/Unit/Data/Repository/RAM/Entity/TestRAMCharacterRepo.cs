using System;
using System.Linq;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.RAM.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity
{
    [TestFixture]
    public class TestRAMCharacterRepo
    {
        private void _assertCharsEqual(Character expected, Character actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.FactionId, actual.FactionId);
            Assert.AreEqual(expected.LocationId, actual.LocationId);
            Assert.AreEqual(expected.EraPoints, actual.EraPoints);
            Assert.AreEqual(expected.EraLevel, actual.EraLevel);
            Assert.AreEqual(expected.TotalPoints, actual.TotalPoints);
            Assert.AreEqual(expected.TotalLevel, actual.TotalLevel);
            Assert.AreEqual(expected.VotePointsLeft, actual.VotePointsLeft);
            Assert.AreEqual(expected.HasBio, actual.HasBio);
            Assert.AreEqual(expected.HasProfilePic, actual.HasProfilePic);
        }

        private DummyRAMCharacterRepo _charRepo;
        private Character _c0;
        private Character _c1;
        private Character _c2;

        [SetUp]
        public void Setup()
        {
            this._charRepo = new DummyRAMCharacterRepo();
            this._c0 = new Character(
                new Name("DIO"),
                new Id(1),
                null,
                null,
                new PointTotal(700),
                new PointTotal(50)
            );
            this._c1 = new Character(
                new Name("Joturo Kujo"),
                new Id(2),
                null,
                null,
                new PointTotal(150),
                new PointTotal(600)
            );
            this._c2 = new Character(
                new Name("Iggy"),
                new Id(2),
                null,
                null,
                new PointTotal(50),
                new PointTotal(700)
            );

            this._charRepo.Insert(this._c0);
            this._charRepo.Insert(this._c1);
            this._charRepo.Insert(this._c2);
            this._charRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            this._charRepo.CleanAll();
            this._charRepo.ResetNextIdValue();
        }

        [Test]
        public void TestGetByPlayerId()
        {
            Assert.Throws<ArgumentException>(()=>
                this._charRepo.GetByPlayerId(new Id(234)));

            var actualId1 = this._charRepo.GetByPlayerId(this._c0.PlayerId);
            Assert.AreEqual(1, actualId1.Count());
            this._assertCharsEqual(this._c0, actualId1.First());

            Assert.AreEqual(this._c1.PlayerId, this._c2.PlayerId);
            var actualId2 =
                this._charRepo.GetByPlayerId(this._c1.PlayerId).ToList();
            Assert.AreEqual(2, actualId2.Count);
            this._assertCharsEqual(this._c1, actualId2[0]);
            this._assertCharsEqual(this._c2, actualId2[1]);
        }

        [Test]
        public void TestFindHighestLevel()
        {
            // TODO: be sure to run all of these tests on both versions of the method
            Level actual = this._charRepo.FindHighestLevel(new Id(2));
            Assert.AreEqual(this._c2.TotalLevel, actual);
            actual = this._charRepo.FindHighestLevel(
                new Player(new Id(2), new Name("Czar"))
            );
            Assert.AreEqual(this._c2.TotalLevel, actual);

            actual = this._charRepo.FindHighestLevel(new Id(1));
            Assert.AreEqual(this._c0.EraLevel, actual);
            actual = this._charRepo.FindHighestLevel(
                new Player(new Id(1), new Name("King"))
            );
            Assert.AreEqual(this._c0.EraLevel, actual);
        }
    }

    public class DummyRAMCharacterRepo
        : RAMCharacterRepo
    {
        public void ResetNextIdValue() { _nextIdValue = 1; }
    }
}