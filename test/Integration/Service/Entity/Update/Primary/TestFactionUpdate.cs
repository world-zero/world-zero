using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Service.Constant.Entity.Primary;
using WorldZero.Service.Entity.Update.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Update.Primary
{
    [TestFixture]
    public class TestFactionUpdate
    {
        private RAMFactionRepo _repo;
        private RAMAbilityRepo _abilityRepo;
        private FactionUpdate _update;
        private IFaction _f0;
        private IAbility _a0;
        private IAbility _a1;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMFactionRepo();
            this._abilityRepo = new RAMAbilityRepo();
            this._update = new FactionUpdate(this._repo, this._abilityRepo);
            this._f0 = new UnsafeFaction(new Name("f0"));
            this._repo.Insert(this._f0);
            this._repo.Save();
            this._a0 = new UnsafeAbility(new Name("a0"), "fsd");
            this._a1 = new UnsafeAbility(new Name("a1"), "fds");
            this._abilityRepo.Insert(this._a0);
            this._abilityRepo.Insert(this._a1);
            this._abilityRepo.Save();
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
        public void TestAmendAbilitySad_IFaction_IAbility()
        {
            IFaction f = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendAbility(f, ConstantAbilities.Historian));

            UnsafeAbility a = new UnsafeAbility(new Name("sdfss"), "dsf");
            Assert.IsNull(this._f0.AbilityId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendAbility(this._f0, a));
            Assert.IsNull(this._repo.GetById(this._f0.Id).AbilityId);
        }

        [Test]
        public void TestAmendAbilityHappy_IFaction_IAbility()
        {
            Assert.IsNull(this._f0.AbilityId);

            this._update.AmendAbility(this._f0, this._a0);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId, this._a0.Id);

            this._update.AmendAbility(this._f0, ConstantAbilities.Historian);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId,ConstantAbilities.Historian.Id);

            IAbility a = null;
            this._update.AmendAbility(this._f0, a);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.IsNull(this._f0.AbilityId);
        }

        [Test]
        public void TestAmendAbilitySad_IFaction_AbilityId()
        {
            IFaction f = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendAbility(f, ConstantAbilities.Historian.Id));

            Name a = new Name("sdfsdfdsd");
            Assert.IsNull(this._f0.AbilityId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendAbility(this._f0, a));
            Assert.IsNull(this._repo.GetById(this._f0.Id).AbilityId);
        }

        [Test]
        public void TestAmendAbilityHappy_IFaction_AbilityId()
        {
            Assert.IsNull(this._f0.AbilityId);

            this._update.AmendAbility(this._f0, this._a0.Id);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId, this._a0.Id);

            this._update.AmendAbility(this._f0, ConstantAbilities.Historian.Id);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId,ConstantAbilities.Historian.Id);

            Name a = null;
            this._update.AmendAbility(this._f0, a);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.IsNull(this._f0.AbilityId);
        }

        [Test]
        public void TestAmendAbilitySad_FactionId_IAbility()
        {
            Name f = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendAbility(f, ConstantAbilities.Historian));

            UnsafeAbility a = new UnsafeAbility(new Name("sdfss"), "dsf");
            Assert.IsNull(this._f0.AbilityId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendAbility(this._f0.Id, a));
            Assert.IsNull(this._repo.GetById(this._f0.Id).AbilityId);
        }

        [Test]
        public void TestAmendAbilityHappy_FactionId_IAbility()
        {
            Assert.IsNull(this._f0.AbilityId);

            this._update.AmendAbility(this._f0.Id, this._a0);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId, this._a0.Id);

            this._update.AmendAbility(this._f0.Id, ConstantAbilities.Historian);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId,ConstantAbilities.Historian.Id);

            IAbility a = null;
            this._update.AmendAbility(this._f0.Id, a);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.IsNull(this._f0.AbilityId);
        }

        [Test]
        public void TestAmendAbilitySad_FactionId_AbilityId()
        {
            Name f = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendAbility(f, ConstantAbilities.Historian.Id));

            Name a = new Name("sdfsdfdsd");
            Assert.IsNull(this._f0.AbilityId);
            Assert.Throws<ArgumentException>(()=>
                this._update.AmendAbility(this._f0.Id, a));
            Assert.IsNull(this._repo.GetById(this._f0.Id).AbilityId);
        }

        [Test]
        public void TestAmendAbilityHappy_FactionId_AbilityId()
        {
            Assert.IsNull(this._f0.AbilityId);

            this._update.AmendAbility(this._f0.Id, this._a0.Id);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId, this._a0.Id);

            this._update.AmendAbility(this._f0.Id, ConstantAbilities.Historian.Id);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.AreEqual(this._f0.AbilityId,ConstantAbilities.Historian.Id);

            Name a = null;
            this._update.AmendAbility(this._f0.Id, a);
            this._f0 = this._repo.GetById(this._f0.Id);
            Assert.IsNull(this._f0.AbilityId);
        }
    }
}