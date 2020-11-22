using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Service.Interface.Entity.Deletion;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntityDel
    {
        private IPlayerRepo _repo;
        private TestEntityDelete _del;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMPlayerRepo();
            this._del = new TestEntityDelete(this._repo);
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
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityDelete(null));
        }

        [Test]
        public void TestDelete()
        {
            var badE = new Player(new Id(2324), new Name("x"));
            this._del.Delete(badE);

            badE = new Player(new Name("xd"));
            this._del.Delete(badE);

            var e = new Player(new Name("x"));
            this._repo.Insert(e);
            this._repo.Save();
            this._del.Delete(e);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(e.Id));
        }
    }

    public class TestEntityDelete
        : IEntityDel<Player, Id, int>
    {
        public TestEntityDelete(IPlayerRepo repo)
            : base(repo)
        { }
    }
}