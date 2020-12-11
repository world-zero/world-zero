using System;
using System.Linq;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Primary
{
    [TestFixture]
    public class TestRAMFactionRepo
    {
        private RAMFactionRepo _factionRepo;
        private UnsafeFaction _f0;
        private UnsafeFaction _f1;
        private UnsafeFaction _f2;

        [SetUp]
        public void Setup()
        {
            this._factionRepo = new RAMFactionRepo();
            this._f0 = new UnsafeFaction(new Name("f0"));
            this._f1 = new UnsafeFaction(new Name("f1"));
            this._f2 = new UnsafeFaction(new Name("f2"));
            this._factionRepo.Insert(this._f0);
            this._factionRepo.Insert(this._f1);
            this._factionRepo.Insert(this._f2);
            this._factionRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            this._factionRepo.CleanAll();
        }

        [Test]
        public void TestGetByAbilityId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._factionRepo.GetByAbilityId(null));
            Assert.Throws<ArgumentException>(()=>
                this._factionRepo.GetByAbilityId(new Name("fds")));

            var abilityRepo = new RAMAbilityRepo();
            var ability0 = new UnsafeAbility(new Name("a0"), "vxk");
            var ability1 = new UnsafeAbility(new Name("a1"), "fdsf");
            abilityRepo.Insert(ability0);
            abilityRepo.Insert(ability1);
            abilityRepo.Save();

            this._f0.AbilityId = ability0.Id;
            this._factionRepo.Update(this._f0);
            this._factionRepo.Save();
            var factiones = this._factionRepo
                .GetByAbilityId(ability0.Id).ToList<UnsafeFaction>();
            Assert.AreEqual(1, factiones.Count());
            foreach (UnsafeFaction f in factiones)
                Assert.AreEqual(this._f0.Id, f.Id);

            this._f1.AbilityId = ability0.Id;
            this._factionRepo.Update(this._f1);
            this._factionRepo.Save();
            factiones = this._factionRepo
                .GetByAbilityId(ability0.Id).ToList<UnsafeFaction>();
            Assert.AreEqual(2, factiones.Count());
            Assert.AreEqual(this._f0.Id, factiones[0].Id);
            Assert.AreEqual(this._f1.Id, factiones[1].Id);

            this._f2.AbilityId = ability1.Id;
            this._factionRepo.Update(this._f2);
            this._factionRepo.Save();
            factiones = this._factionRepo
                .GetByAbilityId(ability1.Id).ToList<UnsafeFaction>();
            Assert.AreEqual(1, factiones.Count());
            foreach (UnsafeFaction f in factiones)
                Assert.AreEqual(this._f2.Id, f.Id);
        }
    }
}