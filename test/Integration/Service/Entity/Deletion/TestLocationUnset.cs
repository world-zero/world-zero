using System;
using System.Linq;
using NUnit.Framework;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Registration.Primary;

// NOTE: This is the only class that tests `Unset()`.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestLocationUnset
    {
        private ILocationRepo _localRepo;
        private ICharacterRepo _charRepo;
        private LocationUnset _unset;
        private Location _l;
        private LocationReg _reg;

        [SetUp]
        public void Setup()
        {
            this._localRepo = new RAMLocationRepo();
            this._charRepo = new RAMCharacterRepo();
            this._unset = new LocationUnset(
                this._localRepo,
                this._charRepo
            );
            this._l = new Location(
                new Name("Oregon City"),
                new Name("Oregon"),
                new Name("USA"),
                new Name("97045")
            );
            this._reg = new LocationReg(this._localRepo);
            this._reg.Register(this._l);
        }

        [TearDown]
        public void TearDown()
        {
            if (this._charRepo.IsTransactionActive())
            {
                this._charRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._charRepo.CleanAll();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>new LocationUnset(
                null,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new LocationUnset(
                this._localRepo,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new LocationUnset(
                null,
                this._charRepo
            ));
        }

        [Test]
        public void TestUnsetSad()
        {
            this._unset.Unset(new Id(100000));
            this._unset.Unset(this._l.Id);
        }

        [Test]
        public void TestUnsetHappy()
        {
            var c = new Character(
                new Name("Jack"),
                new Id(10000),
                locationId: this._l.Id
            );
            this._charRepo.Insert(c);
            this._charRepo.Save();
            var chars = this._charRepo
                .GetByLocationId(this._l.Id).ToList<Character>();
            Assert.AreEqual(1, chars.Count);
            Assert.AreEqual(c.Id, chars[0].Id);
            this._unset.Unset(this._l.Id);
            Assert.Throws<ArgumentException>(()=>
                this._charRepo.GetByLocationId(this._l.Id));
            var newC = this._charRepo.GetById(c.Id);
            Assert.IsNull(newC.LocationId);
        }

        [Test]
        public void TestDelete()
        {
            var c = new Character(
                new Name("Jack"),
                new Id(10000),
                locationId: this._l.Id
            );
            this._charRepo.Insert(c);
            this._charRepo.Save();
            this._unset.Delete(this._l);
            Assert.IsNull(this._charRepo.GetById(c.Id).LocationId);
        }
    }
}