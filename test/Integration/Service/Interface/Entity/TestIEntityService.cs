using System;
using NUnit.Framework;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Interface.Entity;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntityService
    {
        private DummyRAMCommentRepo _repo;
        private TestEntityService _service;
        private int _x;

        [SetUp]
        public void Setup()
        {
            this._x = 0;
            this._repo = new DummyRAMCommentRepo();
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

            var repo = new RAMUnsafeCommentRepo();
            var service = new TestEntityService(repo);
            Assert.IsNotNull(service.Repo);
            Assert.AreEqual(repo, service.Repo);
        }

        [Test]
        public void TestEnsureExists()
        {
            var c = new UnsafeComment(new Id(3), new Id(432), "fds");
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(c.Id));
            this._service.EnsureExists(c);
            Assert.IsNotNull(this._repo.GetById(c.Id));
            this._service.EnsureExists(c);
            Assert.IsNotNull(this._repo.GetById(c.Id));
        }

        [Test]
        public void TestTxnHappy()
        {
            void F(int x)
            {
                this._x += x;
            }

            Assert.AreEqual(0, this._x);
            this._service.Transaction<int>(F, 2);
            Assert.AreEqual(2, this._x);
        }

        [Test]
        public void TestTxnNullDelegate()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._service.Transaction<int>(null, 3));
        }

        [Test]
        public void TestTxnFThrowsArgException()
        {
            void ExcF(int x)
            {
                var id0 = new Id(1);
                var id1 = new Id(2);
                var id2 = new Id(3);
                this._repo.Insert(new UnsafeComment(id0, id1, "f"));
                this._repo.Insert(new UnsafeComment(id1, id2, "f"));
                this._repo.Save();
                this._repo.Insert(new UnsafeComment(id0, id1, "f"));
                throw new ArgumentException("sdfasdf");
            }

            this._repo.Clean();
            Assert.Throws<ArgumentException>(()=>
                this._service.Transaction<int>(ExcF, 3));
            Assert.AreEqual(0, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
        }

        [Test]
        public void TestTxnFThrowsInvalidOpException()
        {
            void ExcF(int x)
            {
                var id0 = new Id(1);
                var id1 = new Id(2);
                var id2 = new Id(3);
                this._repo.Insert(new UnsafeComment(id0, id1, "f"));
                this._repo.Insert(new UnsafeComment(id1, id2, "f"));
                this._repo.Save();
                this._repo.Insert(new UnsafeComment(id0, id1, "f"));
                throw new InvalidOperationException("sdfasdf");
            }

            this._repo.Clean();
            Assert.Throws<InvalidOperationException>(()=>
                this._service.Transaction<int>(ExcF, 3));
            Assert.AreEqual(0, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
        }

        [Test]
        public void TestTxnEndTransactionFails()
        {
            // This will insert an entity with an already saved ID, which will
            // cause Save() to fail in EndTransaction(), all to test that Txn()
            // will catch that exception and throw it's own.

            this._repo.CleanAll();
            this._repo.Insert(new UnsafeComment(new Id(1), new Id(2), "fd"));
            this._repo.Save();

            void F(int x)
            {
                this._repo.Insert(new UnsafeComment(new Id(1), new Id(2), "fd"));
            }

            Assert.Throws<ArgumentException>(()=>
                this._service.Transaction<int>(F, 3));
        }
    }

    public class DummyRAMCommentRepo : RAMUnsafeCommentRepo
    {
        public int SavedCount { get { return this._saved.Count; } }
        public int StagedCount { get { return this._staged.Count; } }
    }

    public class TestEntityService
        : IEntityService<UnsafeComment, Id, int>
    {
        public TestEntityService(IUnsafeCommentRepo commentRepo)
            : base(commentRepo)
        { }

        public IEntityRepo<UnsafeComment, Id, int> Repo
        { get { return this._repo; } }

        public new void EnsureExists(UnsafeComment c)
        {
            base.EnsureExists(c);
        }
    }
}