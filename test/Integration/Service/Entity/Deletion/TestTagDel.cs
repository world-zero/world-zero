using System;
using System.Linq;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity.Primary;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestTagDel
    {
        private Tag _t0;
        private Tag _t1;
        private RAMTagRepo _tagRepo;
        private RAMTaskTagRepo _taskTagRepo;
        private RAMMetaTaskTagRepo _mtTagRepo;
        private RAMPraxisTagRepo _praxisTagRepo;
        private TaskTagDel _taskTagDel;
        private MetaTaskTagDel _mtTagDel;
        private PraxisTagDel _praxisTagDel;
        private TagDel _del;

        [SetUp]
        public void Setup()
        {
            this._t0 = new Tag(new Name("#valid"));
            this._t1 = new Tag(new Name("#invalid"));

            this._tagRepo = new RAMTagRepo();
            this._tagRepo.Insert(this._t0);
            this._tagRepo.Insert(this._t1);
            this._tagRepo.Save();

            this._taskTagRepo = new RAMTaskTagRepo();
            this._taskTagRepo.Insert(new TaskTag(new Id(100), this._t0.Id));
            this._taskTagRepo.Insert(new TaskTag(new Id(200), this._t0.Id));
            this._taskTagRepo.Insert(new TaskTag(new Id(300), this._t1.Id));
            this._taskTagRepo.Save();

            this._mtTagRepo = new RAMMetaTaskTagRepo();
            this._mtTagRepo.Insert(new MetaTaskTag(new Id(400), this._t0.Id));
            this._mtTagRepo.Insert(new MetaTaskTag(new Id(500), this._t1.Id));
            this._mtTagRepo.Insert(new MetaTaskTag(new Id(600), this._t1.Id));
            this._mtTagRepo.Save();

            this._praxisTagRepo = new RAMPraxisTagRepo();
            this._praxisTagRepo.Insert(new PraxisTag(new Id(70), this._t0.Id));
            this._praxisTagRepo.Insert(new PraxisTag(new Id(80), this._t1.Id));
            this._praxisTagRepo.Insert(new PraxisTag(new Id(90), this._t1.Id));
            this._praxisTagRepo.Save();

            this._taskTagDel = new TaskTagDel(this._taskTagRepo);
            this._mtTagDel = new MetaTaskTagDel(this._mtTagRepo);
            this._praxisTagDel = new PraxisTagDel(this._praxisTagRepo);

            this._del = new TagDel(
                this._tagRepo,
                this._taskTagDel,
                this._mtTagDel,
                this._praxisTagDel
            );
        }

        [TearDown]
        public void TearDown()
        {
            if (this._tagRepo.IsTransactionActive())
            {
                this._tagRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._tagRepo.CleanAll();
        }

        [Test]
        public void TestDeleteSad()
        {
            Tag t = null;
            Name n = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(t));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(n));
        }

        [Test]
        public void TestDelete()
        {
            Assert.AreEqual(2,
                this._taskTagRepo.GetByRightId(this._t0.Id).Count());
            Assert.AreEqual(1,
                this._taskTagRepo.GetByRightId(this._t1.Id).Count());
            Assert.AreEqual(1,
                this._mtTagRepo.GetByRightId(this._t0.Id).Count());
            Assert.AreEqual(2,
                this._mtTagRepo.GetByRightId(this._t1.Id).Count());
            Assert.AreEqual(1,
                this._praxisTagRepo.GetByRightId(this._t0.Id).Count());
            Assert.AreEqual(2,
                this._praxisTagRepo.GetByRightId(this._t1.Id).Count());

            this._del.Delete(this._t0);

            Assert.Throws<ArgumentException>(()=>
                this._tagRepo.GetById(this._t0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._taskTagRepo.GetByRightId(this._t0.Id).Count());
            Assert.AreEqual(1,
                this._taskTagRepo.GetByRightId(this._t1.Id).Count());
            Assert.Throws<ArgumentException>(()=>
                this._mtTagRepo.GetByRightId(this._t0.Id).Count());
            Assert.AreEqual(2,
                this._mtTagRepo.GetByRightId(this._t1.Id).Count());
            Assert.Throws<ArgumentException>(()=>
                this._praxisTagRepo.GetByRightId(this._t0.Id).Count());
            Assert.AreEqual(2,
                this._praxisTagRepo.GetByRightId(this._t1.Id).Count());
        }
    }
}