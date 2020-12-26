using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity.Update.Primary
{
    [TestFixture]
    public class TestABCIdNamedEntityUpdate
    {
        private Name _oldName;
        private Name _newName;
        private UnsafeCharacter _c;
        private RAMCharacterRepo _repo;
        private TestIdNamedEntityUpdate _update;

        [SetUp]
        public void Setup()
        {
            this._oldName = new Name("Old");
            this._newName = new Name("Old");
            this._c = new UnsafeCharacter(this._oldName, new Id(2));
            this._repo = new RAMCharacterRepo();
            this._repo.Insert(this._c);
            this._repo.Save();
            this._update = new TestIdNamedEntityUpdate(this._repo);
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
        public void TestAmendNameHappy()
        {
            Assert.AreEqual(this._c.Name, this._oldName);
            this._update.AmendName(this._c, this._newName);
            Assert.AreEqual(this._c.Name, this._newName);
        }

        [Test]
        public void TestAmendNameSad()
        {
            ICharacter c = null;
            Id id = null;
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendName(
                c, this._oldName));
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendName(
                id, this._oldName));
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendName(
                this._c.Id, null));
        }
    }

    public class TestIdNamedEntityUpdate : ABCIdNamedEntityUpdate<ICharacter>
    {
        public TestIdNamedEntityUpdate(ICharacterRepo repo)
            : base(repo)
        { }
    }
}