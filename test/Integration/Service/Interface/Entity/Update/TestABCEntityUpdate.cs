using System;
using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity.Update.Primary
{
    [TestFixture]
    public class TestABCEntityUpdate
    {
        private Name _name;
        private string _desc;
        private string _newDesc;
        private IAbility _a;
        private RAMAbilityRepo _repo;
        private TestEntityUpdate _update;

        private void _assertIsOld()
        {
            Assert.AreEqual(
                this._desc,
                this._repo.GetById(this._a.Id).Description);
        }

        private void _assertIsNew()
        {
            Assert.AreEqual(
                this._newDesc,
                this._repo.GetById(this._a.Id).Description);
        }

        [SetUp]
        public void Setup()
        {
            this._name = new Name("Test");
            this._desc = "Thi sis a tets";
            this._newDesc = "A new desc";
            this._a = new UnsafeAbility(this._name, this._desc);
            this._repo = new RAMAbilityRepo();
            this._repo.Insert(this._a);
            this._repo.Save();
            this._update = new TestEntityUpdate(this._repo);
            this._assertIsOld();
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
        public void TestAmendDescriptionGivenAbilitySad()
        {
            IAbility a = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendDescription(a, this._newDesc));
            this._assertIsOld();

            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendDescription(this._a, null));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendDescription(this._a, ""));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendDescription(this._a, "   "));
            this._assertIsOld();
        }

        [Test]
        public void TestAmendDescriptionGivenAbilityHappy()
        {
            this._assertIsOld();
            this._update.AmendDescription(this._a, this._newDesc);
            this._assertIsNew();

            this._update.AmendDescription(this._a, this._desc);
            this._assertIsOld();
        }

        [Test]
        public void TestAmendDescriptionGivenIdSad()
        {
            Name id = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendDescription(id, this._newDesc));
            this._assertIsOld();

            Assert.Throws<ArgumentNullException>(()=>
                this._update.AmendDescription(this._a.Id, null));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendDescription(this._a.Id, ""));
            this._assertIsOld();

            Assert.Throws<ArgumentException>(()=>
                this._update.AmendDescription(this._a.Id, "   "));
            this._assertIsOld();
        }

        [Test]
        public void TestAmendDescriptionGivenIdHappy()
        {
            this._assertIsOld();
            this._update.AmendDescription(this._a.Id, this._newDesc);
            this._assertIsNew();

            this._update.AmendDescription(this._a.Id, this._desc);
            this._assertIsOld();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>new TestEntityUpdate(null));
        }
    }

    public class TestEntityUpdate
        : ABCEntityUpdate<IAbility, Name, string>,
        IAbilityUpdate
    {
        public TestEntityUpdate(IAbilityRepo repo)
            : base(repo)
        { }

        public void AmendDescription(IAbility a, string newDesc)
        {
            this.AssertNotNull(a, "a");
            this.AssertNotNull(newDesc, "newDesc");
            void f()
            {
                ((UnsafeAbility) a).Description = newDesc;
            }
            this.AmendHelper<IAbility>(f, a, false);
        }

        public void AmendDescription(Name abilityId, string newDesc)
        {
            this.AssertNotNull(abilityId, "abilityId");
            this.AssertNotNull(newDesc, "newDesc");
            void f()
            {
                this.AmendDescription(this._repo.GetById(abilityId), newDesc);
            }
            this.Transaction(f, true);
        }

        public async Task AmendDescriptionAsync(IAbility a, string newDesc)
        {
            this.AssertNotNull(a, "a");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(a, newDesc));
        }

        public async Task AmendDescriptionAsync(Name abilityId, string newDesc)
        {
            this.AssertNotNull(abilityId, "abilityId");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescriptionAsync(abilityId, newDesc));
        }
    }
}