using System;
using NUnit.Framework;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Interface.Entity.Generic;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntityService
    {
        private DummyRAMCommentRepo _repo;
        private TestEntityService _service;

        [SetUp]
        public void Setup()
        {
            this._repo = new DummyRAMCommentRepo();
            this._service = new TestEntityService(this._repo);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityService(null));

            var repo = new RAMCommentRepo();
            var service = new TestEntityService(repo);
            Assert.IsNotNull(service.Repo);
            Assert.AreEqual(repo, service.Repo);
        }
    }

    public class DummyRAMCommentRepo : RAMCommentRepo
    {
        public int SavedCount { get { return this._saved.Count; } }
        public int StagedCount { get { return this._staged.Count; } }
    }

    public class TestEntityService
        : ABCEntityService<IComment, Id, int>
    {
        public TestEntityService(ICommentRepo commentRepo)
            : base((IEntityRepo<IComment, Id, int>) commentRepo)
        { }

        public IEntityRepo<IComment, Id, int> Repo
        { get { return this._repo; } }
    }
}