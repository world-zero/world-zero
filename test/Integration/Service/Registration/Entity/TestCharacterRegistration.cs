using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Service.Registration.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity
{
    [TestFixture]
    public class TestCharacterRegistration
    {
        private ICharacterRepo _characterRepo;
        private IFactionRepo _factionRepo;
        private IPlayerRepo _playerRepo;
        private ILocationRepo _locationRepo;
        private CharacterRegistration _registration;
        private Player _player0;
        private Faction _faction0;
        private Location _location0;

        [SetUp]
        public void Setup()
        {
            this._characterRepo = new RAMCharacterRepo();
            this._factionRepo = new RAMFactionRepo();
            this._playerRepo = new RAMPlayerRepo();
            this._locationRepo = new RAMLocationRepo();
            this._registration = new CharacterRegistration(
                this._characterRepo,
                this._playerRepo,
                this._factionRepo,
                this._locationRepo
            );
            this._player0 = new Player(new Name("Johnothan Jostar"));
            this._playerRepo.Insert(this._player0);
            this._playerRepo.Save();
            this._faction0 = new Faction(
                new Name("The JoJos"),
                new PastDate(DateTime.UtcNow)
            );
            this._factionRepo.Insert(this._faction0);
            this._factionRepo.Save();
            this._location0 = new Location(
                new Name("oc"),
                new Name("or"),
                new Name("hell"),
                new Name("97045")
            );
            this._locationRepo.Insert(this._location0);
            this._locationRepo.Save();
        }

        [Test]
        public void TestRegisterHappy()
        {
            var c = new Character(
                new Name("something"),
                this._player0.Id,
                this._faction0.Id,
                this._location0.Id
            );
            Assert.IsFalse(c.IsIdSet());
            this._registration.Register(c);
            Assert.IsTrue(c.IsIdSet());
            Assert.IsNotNull(this._characterRepo.GetById(c.Id));

            c = new Character(
                new Name("alt"),
                this._player0.Id,
                this._faction0.Id
            );
            Assert.IsFalse(c.IsIdSet());
            this._registration.Register(c);
            Assert.IsTrue(c.IsIdSet());
            Assert.IsNotNull(this._characterRepo.GetById(c.Id));

            c = new Character(
                new Name("foo"),
                this._player0.Id
            );
            Assert.IsFalse(c.IsIdSet());
            this._registration.Register(c);
            Assert.IsTrue(c.IsIdSet());
            Assert.IsNotNull(this._characterRepo.GetById(c.Id));
        }

        [Test]
        public void TestRegisterSad()
        {
            // all good foreign keys
            var c = new Character(
                new Name("something"),
                this._player0.Id,
                this._faction0.Id,
                this._location0.Id
            );

            // all bad foreign keys
            c = new Character(
                new Name("dummy character"),
                new Id(9001),
                new Name("fake"),
                new Id(9002)
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(c)
            );

            // only the first is invalid
            c = new Character(
                new Name("dummy character"),
                new Id(9001),
                this._faction0.Id,
                this._location0.Id
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(c)
            );

            // only the second is invalid
            c = new Character(
                new Name("dummy character"),
                this._player0.Id,
                new Name("asdf"),
                this._location0.Id
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(c)
            );

            // only the third is invalid
            c = new Character(
                this._player0.Id,
                this._faction0.Id,
                new Id(9001)
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(c)
            );
        }

        [Test]
        public void TestConstructor()
        {
            new CharacterRegistration(
                this._characterRepo,
                this._playerRepo,
                this._factionRepo,
                this._locationRepo
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterRegistration(
                    null,
                    this._playerRepo,
                    this._factionRepo,
                    this._locationRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterRegistration(
                    this._characterRepo,
                    null,
                    this._factionRepo,
                    this._locationRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterRegistration(
                    this._characterRepo,
                    this._playerRepo,
                    null,
                    this._locationRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterRegistration(
                    this._characterRepo,
                    this._playerRepo,
                    this._factionRepo,
                    null
                )
            );
        }
    }
}