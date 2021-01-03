using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Entity.Update.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Update.Primary
{
    [TestFixture]
    public class TestCharacterUpdate
    {
        private RAMCharacterRepo _repo;
        private RAMFactionRepo _factionRepo;
        private RAMLocationRepo _locationRepo;
        private CharacterUpdate _update;
        private ICharacter _c0;
        private IFaction _f0;
        private IFaction _f1;
        private ILocation _l0;
        private ILocation _l1;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMCharacterRepo();
            this._factionRepo = new RAMFactionRepo();
            this._locationRepo = new RAMLocationRepo();
            this._update = new CharacterUpdate(
                this._repo,
                this._factionRepo,
                this._locationRepo
            );
            this._c0 = new UnsafeCharacter(new Name("f0"), new Id(2));
            this._repo.Insert(this._c0);
            this._repo.Save();
            this._f0 = new UnsafeFaction(new Name("f0"));
            this._f1 = new UnsafeFaction(new Name("f1"));
            this._factionRepo.Insert(this._f0);
            this._factionRepo.Insert(this._f1);
            this._factionRepo.Save();
            this._l0 = new UnsafeLocation(
                new Name("city0"),
                new Name("state0"),
                new Name("country0"),
                new Name("zip0")
            );
            this._l1 = new UnsafeLocation(
                new Name("city1"),
                new Name("state1"),
                new Name("country1"),
                new Name("zip1")
            );
            this._locationRepo.Insert(this._l0);
            this._locationRepo.Insert(this._l1);
            this._locationRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._repo.IsTransactionActive())
            {
                this._repo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._repo.CleanAll();
        }

        [Test]
        public void TestAmendFactionSad_ICharacter_IFaction()
        {
            ICharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendFaction(c, this._f0));

            UnsafeFaction f = new UnsafeFaction(new Name("sdfss"));
            Assert.IsNull(this._c0.FactionId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendFaction(this._c0, f));
            Assert.IsNull(this._repo.GetById(this._c0.Id).FactionId);
        }

        [Test]
        public void TestAmendFactionHappy_ICharacter_IFaction()
        {
            Assert.IsNull(this._c0.FactionId);

            this._update.AmendFaction(this._c0, this._f0);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId, this._f0.Id);

            this._update.AmendFaction(this._c0, this._f1);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId,this._f1.Id);

            IFaction f = null;
            this._update.AmendFaction(this._c0, f);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.FactionId);
        }

        [Test]
        public void TestAmendFactionSad_ICharacter_FactionId()
        {
            ICharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendFaction(c, this._f0.Id));

            Name a = new Name("sdfsdfdsd");
            Assert.IsNull(this._c0.FactionId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendFaction(this._c0, a));
            Assert.IsNull(this._repo.GetById(this._c0.Id).FactionId);
        }

        [Test]
        public void TestAmendFactionHappy_ICharacter_FactionId()
        {
            Assert.IsNull(this._c0.FactionId);

            this._update.AmendFaction(this._c0, this._f0.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId, this._f0.Id);

            this._update.AmendFaction(this._c0, this._f1.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId, this._f1.Id);

            Name a = null;
            this._update.AmendFaction(this._c0, a);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.FactionId);
        }

        [Test]
        public void TestAmendFactionSad_CharacterId_IFaction()
        {
            Id c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendFaction(c, this._f0));

            UnsafeFaction f = new UnsafeFaction(new Name("sdfss"));
            Assert.IsNull(this._c0.FactionId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendFaction(this._c0.Id, f));
            Assert.IsNull(this._repo.GetById(this._c0.Id).FactionId);
        }

        [Test]
        public void TestAmendFactionHappy_CharacterId_IFaction()
        {
            Assert.IsNull(this._c0.FactionId);

            this._update.AmendFaction(this._c0.Id, this._f0);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId, this._f0.Id);

            this._update.AmendFaction(this._c0.Id, this._f1);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId, this._f1.Id);

            IFaction a = null;
            this._update.AmendFaction(this._c0.Id, a);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.FactionId);
        }

        [Test]
        public void TestAmendFactionSad_CharacterId_FactionId()
        {
            Id c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendFaction(c, this._f0.Id));

            Name a = new Name("sdfsdfdsd");
            Assert.IsNull(this._c0.FactionId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendFaction(this._c0.Id, a));
            Assert.IsNull(this._repo.GetById(this._c0.Id).FactionId);
        }

        [Test]
        public void TestAmendFactionHappy_CharacterId_FactionId()
        {
            Assert.IsNull(this._c0.FactionId);

            this._update.AmendFaction(this._c0.Id, this._f0.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId, this._f0.Id);

            this._update.AmendFaction(this._c0.Id, this._f1.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.FactionId, this._f1.Id);

            Name a = null;
            this._update.AmendFaction(this._c0.Id, a);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.FactionId);
        }

        // --------------------------------------------------------------------

        [Test]
        public void TestAmendLocationSad_ICharacter_ILocation()
        {
            ICharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendLocation(c, this._l0));

            UnsafeLocation l = new UnsafeLocation(
                new Name("sdfss"),
                new Name("sdfss"),
                new Name("sdfss"),
                new Name("sdfss")
            );
            Assert.IsNull(this._c0.LocationId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendLocation(this._c0, l));
            Assert.IsNull(this._repo.GetById(this._c0.Id).LocationId);
        }

        [Test]
        public void TestAmendLocationHappy_ICharacter_ILocation()
        {
            Assert.IsNull(this._c0.LocationId);

            this._update.AmendLocation(this._c0, this._l0);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId, this._l0.Id);

            this._update.AmendLocation(this._c0, this._l1);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId,this._l1.Id);

            ILocation l = null;
            this._update.AmendLocation(this._c0, l);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.LocationId);
        }

        [Test]
        public void TestAmendLocationSad_ICharacter_LocationId()
        {
            ICharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendLocation(c, this._l0.Id));

            Id a = new Id(3242);
            Assert.IsNull(this._c0.LocationId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendLocation(this._c0, a));
            Assert.IsNull(this._repo.GetById(this._c0.Id).LocationId);
        }

        [Test]
        public void TestAmendLocationHappy_ICharacter_LocationId()
        {
            Assert.IsNull(this._c0.LocationId);

            this._update.AmendLocation(this._c0, this._l0.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId, this._l0.Id);

            this._update.AmendLocation(this._c0, this._l1.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId, this._l1.Id);

            Id a = null;
            this._update.AmendLocation(this._c0, a);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.LocationId);
        }

        [Test]
        public void TestAmendLocationSad_CharacterId_ILocation()
        {
            Id c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendLocation(c, this._l0));

            UnsafeLocation f = new UnsafeLocation(
                new Name("sdfss"),
                new Name("sdfss"),
                new Name("sdfss"),
                new Name("sdfss")
            );
            Assert.IsNull(this._c0.LocationId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendLocation(this._c0.Id, f));
            Assert.IsNull(this._repo.GetById(this._c0.Id).LocationId);
        }

        [Test]
        public void TestAmendLocationHappy_CharacterId_ILocation()
        {
            Assert.IsNull(this._c0.LocationId);

            this._update.AmendLocation(this._c0.Id, this._l0);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId, this._l0.Id);

            this._update.AmendLocation(this._c0.Id, this._l1);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId, this._l1.Id);

            ILocation a = null;
            this._update.AmendLocation(this._c0.Id, a);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.LocationId);
        }

        [Test]
        public void TestAmendLocationSad_CharacterId_LocationId()
        {
            Id c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendLocation(c, this._l0.Id));

            Id a = new Id(324342);
            Assert.IsNull(this._c0.LocationId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendLocation(this._c0.Id, a));
            Assert.IsNull(this._repo.GetById(this._c0.Id).LocationId);
        }

        [Test]
        public void TestAmendLocationHappy_CharacterId_LocationId()
        {
            Assert.IsNull(this._c0.LocationId);

            this._update.AmendLocation(this._c0.Id, this._l0.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId, this._l0.Id);

            this._update.AmendLocation(this._c0.Id, this._l1.Id);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.AreEqual(this._c0.LocationId, this._l1.Id);

            Id a = null;
            this._update.AmendLocation(this._c0.Id, a);
            this._c0 = this._repo.GetById(this._c0.Id);
            Assert.IsNull(this._c0.LocationId);
        }
    }
}