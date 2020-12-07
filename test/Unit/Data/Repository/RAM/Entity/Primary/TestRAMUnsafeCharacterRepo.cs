using System;
using System.Linq;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Primary
{
    [TestFixture]
    public class TestRAMCharacterRepo
    {
        private void _assertCharsEqual(UnsafeCharacter expected, UnsafeCharacter actual)
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
        private UnsafeCharacter _c0;
        private UnsafeCharacter _c1;
        private UnsafeCharacter _c2;

        [SetUp]
        public void Setup()
        {
            this._charRepo = new DummyRAMCharacterRepo();
            this._c0 = new UnsafeCharacter(
                new Name("DIO"),
                new Id(1),
                null,
                null,
                new PointTotal(700),
                new PointTotal(50)
            );
            this._c1 = new UnsafeCharacter(
                new Name("Joturo Kujo"),
                new Id(2),
                null,
                null,
                new PointTotal(150),
                new PointTotal(600)
            );
            this._c2 = new UnsafeCharacter(
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
        public void TestGetByLocationId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._charRepo.GetByLocationId(null));
            Assert.Throws<ArgumentException>(()=>
                this._charRepo.GetByLocationId(new Id(9000)));

            var locRepo = new RAMLocationRepo();
            var loc0 = new Location(
                new Name("Oregon City"),
                new Name("Oregon"),
                new Name("USA"),
                new Name("97045")
            );
            var loc1 = new Location(
                new Name("Portland"),
                new Name("Oregon"),
                new Name("USA"),
                new Name("920-idk")
            );
            locRepo.Insert(loc0);
            locRepo.Insert(loc1);
            locRepo.Save();

            this._c0.LocationId = loc0.Id;
            this._charRepo.Update(this._c0);
            this._charRepo.Save();
            var chars = this._charRepo
                .GetByLocationId(loc0.Id).ToList<UnsafeCharacter>();
            Assert.AreEqual(1, chars.Count());
            foreach (UnsafeCharacter c in chars)
                Assert.AreEqual(this._c0.Id, c.Id);

            this._c1.LocationId = loc0.Id;
            this._charRepo.Update(this._c1);
            this._charRepo.Save();
            chars = this._charRepo
                .GetByLocationId(loc0.Id).ToList<UnsafeCharacter>();
            Assert.AreEqual(2, chars.Count());
            Assert.AreEqual(this._c0.Id, chars[0].Id);
            Assert.AreEqual(this._c1.Id, chars[1].Id);

            this._c2.LocationId = loc1.Id;
            this._charRepo.Update(this._c2);
            this._charRepo.Save();
            chars = this._charRepo
                .GetByLocationId(loc1.Id).ToList<UnsafeCharacter>();
            Assert.AreEqual(1, chars.Count());
            foreach (UnsafeCharacter c in chars)
                Assert.AreEqual(this._c2.Id, c.Id);
        }

        [Test]
        public void TestGetByFactionId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._charRepo.GetByFactionId(null));
            Assert.Throws<ArgumentException>(()=>
                this._charRepo.GetByFactionId(new Name("xcvs")));

            var factionRepo = new RAMUnsafeFactionRepo();
            var f0 = new UnsafeFaction(new Name("F0"));
            var f1 = new UnsafeFaction(new Name("F1"));
            factionRepo.Insert(f0);
            factionRepo.Insert(f1);
            factionRepo.Save();

            this._c0.FactionId = f0.Id;
            this._charRepo.Update(this._c0);
            this._charRepo.Save();
            var chars = this._charRepo
                .GetByFactionId(f0.Id).ToList<UnsafeCharacter>();
            Assert.AreEqual(1, chars.Count());
            foreach (UnsafeCharacter c in chars)
                Assert.AreEqual(this._c0.Id, c.Id);

            this._c1.FactionId = f0.Id;
            this._charRepo.Update(this._c1);
            this._charRepo.Save();
            chars = this._charRepo
                .GetByFactionId(f0.Id).ToList<UnsafeCharacter>();
            Assert.AreEqual(2, chars.Count());
            Assert.AreEqual(this._c0.Id, chars[0].Id);
            Assert.AreEqual(this._c1.Id, chars[1].Id);

            this._c2.FactionId = f1.Id;
            this._charRepo.Update(this._c2);
            this._charRepo.Save();
            chars = this._charRepo
                .GetByFactionId(f1.Id).ToList<UnsafeCharacter>();
            Assert.AreEqual(1, chars.Count());
            foreach (UnsafeCharacter c in chars)
                Assert.AreEqual(this._c2.Id, c.Id);
        }

        [Test]
        public void TestFindHighestLevel()
        {
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
        : RAMUnsafeCharacterRepo
    {
        public void ResetNextIdValue() { _nextIdValue = 1; }
    }
}