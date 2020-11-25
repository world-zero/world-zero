using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.RAM.Entity.Primary;
using WorldZero.Service.Entity.Registration.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestCharacterReg
    {
        private ICharacterRepo _characterRepo;
        private IFactionRepo _factionRepo;
        private IPlayerRepo _playerRepo;
        private ILocationRepo _locationRepo;
        private CharacterReg _registration;
        private Player _player0;
        private Faction _faction0;
        private Location _location0;

        [SetUp]
        public void Setup()
        {
            CharacterReg.MinLevelToRegister = new Level(3);
            this._characterRepo = new RAMCharacterRepo();
            this._factionRepo = new RAMFactionRepo();
            this._playerRepo = new DummyRAMPlayerRepo();
            this._locationRepo = new DummyRAMLocationRepo();
            this._registration = new CharacterReg(
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

        [TearDown]
        public void TearDown()
        {
            if (this._playerRepo.IsTransactionActive())
            {
                this._playerRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._playerRepo.CleanAll();
            ((DummyRAMPlayerRepo) this._playerRepo).ResetNextIdValue();
            ((DummyRAMLocationRepo) this._locationRepo).ResetNextIdValue();
        }

        [Test]
        public void TestRegisterHappy()
        {
            var c = new Character(
                new Name("something"),
                this._player0.Id,
                this._faction0.Id,
                this._location0.Id,
                new PointTotal(600)
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
            // All good foreign keys, this is a happy run.
            var c = new Character(
                new Name("something"),
                this._player0.Id,
                this._faction0.Id,
                this._location0.Id
            );

            // Make sure that a player with an insufficient level cannot
            // register another character.
            this._registration.Register(c);
            Assert.Throws<ArgumentException>(()=>this._registration.Register(
                new Character(new Name("f"), new Id(4324))
            ));

            // All bad foreign keys.
            c = new Character(
                new Name("dummy character"),
                new Id(9001),
                new Name("fake"),
                new Id(9002)
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(c)
            );

            // Only the first is invalid.
            c = new Character(
                new Name("dummy character"),
                new Id(9001),
                this._faction0.Id,
                this._location0.Id
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(c)
            );

            // Only the second is invalid.
            c = new Character(
                new Name("dummy character"),
                this._player0.Id,
                new Name("asdf"),
                this._location0.Id
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(c)
            );

            // Only the third is invalid.
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
            new CharacterReg(
                this._characterRepo,
                this._playerRepo,
                this._factionRepo,
                this._locationRepo
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterReg(
                    null,
                    this._playerRepo,
                    this._factionRepo,
                    this._locationRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterReg(
                    this._characterRepo,
                    null,
                    this._factionRepo,
                    this._locationRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterReg(
                    this._characterRepo,
                    this._playerRepo,
                    null,
                    this._locationRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new CharacterReg(
                    this._characterRepo,
                    this._playerRepo,
                    this._factionRepo,
                    null
                )
            );
        }

        [Test]
        public void TestCanRegCharacterNullChecks()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._registration.CanRegCharacter((Player) null));
            Assert.Throws<ArgumentNullException>(()=>
                this._registration.CanRegCharacter((Id) null));

            Level old = CharacterReg.MinLevelToRegister;
            CharacterReg.MinLevelToRegister = null;
            Assert.Throws<ArgumentException>(()=>
                this._registration.CanRegCharacter(new Player(new Name("f"))));
            Assert.Throws<ArgumentException>(()=>
                this._registration.CanRegCharacter(new Id(234)));
            CharacterReg.MinLevelToRegister = old;
        }

        [Test]
        public void TestCanRegCharacterNoAssociatedPlayerId()
        {
            Assert.IsTrue(this._registration.CanRegCharacter(
                new Player(new Id(342), new Name("f"))));
            Assert.IsTrue(this._registration.CanRegCharacter(new Id(342)));
        }

        [Test]
        public void TestCanRegCharacterHappy()
        {
            this._registration.Register(new Character(
                new Name("f"),
                this._player0.Id,
                null,
                null,
                new PointTotal(3000)
            ));
            Assert.IsTrue(
                this._registration.CanRegCharacter(this._player0.Id));
        }

        [Test]
        public void TestCanRegCharacterSad()
        {
            this._registration.Register(new Character(
                new Name("f"),
                this._player0.Id
            ));
            Assert.IsFalse(
                this._registration.CanRegCharacter(this._player0.Id));
        }
    }

    public class DummyRAMPlayerRepo
        : RAMPlayerRepo
    {
        public void ResetNextIdValue() { _nextIdValue = 1; }
    }

    public class DummyRAMLocationRepo
        : RAMLocationRepo
    {
        public void ResetNextIdValue() { _nextIdValue = 1; }
    }
}