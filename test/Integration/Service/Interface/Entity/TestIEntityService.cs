using System;
using NUnit.Framework;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Service.Interface.Entity;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntityService
    {
        private RAMFlagRepo _repo;
        private TestEntityService _service;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMFlagRepo();
            this._service = new TestEntityService(this._repo);
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
                new TestEntityService(null));

            var repo = new RAMFlagRepo();
            var service = new TestEntityService(repo);
            Assert.IsNotNull(service.Repo);
            Assert.AreEqual(repo, service.Repo);
        }

        [Test]
        public void TestEnsureExists()
        {
            var f = new Flag(new Name("dne"));
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(f.Id));
            this._service.EnsureExists(f);
            Assert.IsNotNull(this._repo.GetById(f.Id));
            this._service.EnsureExists(f);
            Assert.IsNotNull(this._repo.GetById(f.Id));
        }
    }

    public class TestEntityService
        : IEntityService<Flag, Name, string>
    {
        public TestEntityService(IFlagRepo flagRepo)
            : base(flagRepo)
        { }

        public IEntityRepo<Flag, Name, string> Repo { get { return this._repo; } }

        public new void EnsureExists(Flag p)
        {
            base.EnsureExists(p);
        }
    }
}