using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Entity.Registration.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestFactionReg
    {
        private IFactionRepo _factionRepo;
        private IUnsafeAbilityRepo _abilityRepo;
        private FactionReg _registration;
        private Faction _faction0;
        private UnsafeAbility _ability0;

        [SetUp]
        public void Setup()
        {
            this._factionRepo = new RAMFactionRepo();
            this._abilityRepo = new RAMUnsafeAbilityRepo();
            this._registration = new FactionReg(
                this._factionRepo,
                this._abilityRepo
            );
            this._ability0 = new UnsafeAbility(new Name("Ze Worldo"), "Stop time for 5 seconds.");
            this._abilityRepo.Insert(this._ability0);
            this._abilityRepo.Save();
            this._faction0 = new Faction(
                new Name("The JoJos"),
                new PastDate(DateTime.UtcNow),
                null,
                this._ability0.Id
            );
            this._factionRepo.Insert(this._faction0);
            this._factionRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._factionRepo.IsTransactionActive())
            {
                this._factionRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._factionRepo.CleanAll();
        }

        [Test]
        public void TestRegisterHappy()
        {
            var f = new Faction(
                new Name("idk"),
                new PastDate(DateTime.UtcNow),
                null,
                this._ability0.Id
            );
            this._registration.Register(f);
            Assert.IsNotNull(this._factionRepo.GetById(f.Id));

            f = new Faction(
                new Name("something"),
                new PastDate(DateTime.UtcNow)
            );
            this._registration.Register(f);
            Assert.IsNotNull(this._factionRepo.GetById(f.Id));
        }

        [Test]
        public void TestRegisterSad()
        {
            var f = new Faction(
                new Name("idk"),
                new PastDate(DateTime.UtcNow),
                null,
                new Name("INVALID ABILTIY")
            );
            Assert.Throws<ArgumentException>(
                ()=>this._registration.Register(f)
            );
        }

        [Test]
        public void TestConstructor()
        {
            new FactionReg(
                this._factionRepo,
                this._abilityRepo
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new FactionReg(
                    null,
                    this._abilityRepo
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new FactionReg(
                    this._factionRepo,
                    null
                )
            );
            Assert.Throws<ArgumentNullException>(
                ()=>new FactionReg(
                    null,
                    null
                )
            );
        }
    }
}