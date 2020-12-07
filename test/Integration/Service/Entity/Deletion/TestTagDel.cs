using System;
using System.Linq;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestTagDel
    {
        private UnsafeTag _t0;
        private UnsafeTag _t1;
        private RAMUnsafeTagRepo _tagRepo;
        private RAMTaskTagRepo _taskTagRepo;
        private RAMUnsafeMetaTaskTagRepo _mtTagRepo;
        private RAMPraxisTagRepo _praxisTagRepo;
        private TaskTagDel _taskTagDel;
        private MetaTaskTagDel _mtTagDel;
        private PraxisTagDel _praxisTagDel;
        private TagDel _del;

        [SetUp]
        public void Setup()
        {
            this._t0 = new UnsafeTag(new Name("#valid"));
            this._t1 = new UnsafeTag(new Name("#invalid"));

            this._tagRepo = new RAMUnsafeTagRepo();
            this._tagRepo.Insert(this._t0);
            this._tagRepo.Insert(this._t1);
            this._tagRepo.Save();

            this._taskTagRepo = new RAMTaskTagRepo();
            this._taskTagRepo.Insert(new TaskTag(new Id(100), this._t0.Id));
            this._taskTagRepo.Insert(new TaskTag(new Id(200), this._t0.Id));
            this._taskTagRepo.Insert(new TaskTag(new Id(300), this._t1.Id));
            this._taskTagRepo.Save();

            this._mtTagRepo = new RAMUnsafeMetaTaskTagRepo();
            this._mtTagRepo.Insert(new UnsafeMetaTaskTag(new Id(400), this._t0.Id));
            this._mtTagRepo.Insert(new UnsafeMetaTaskTag(new Id(500), this._t1.Id));
            this._mtTagRepo.Insert(new UnsafeMetaTaskTag(new Id(600), this._t1.Id));
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
            UnsafeTag t = null;
            Name n = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(t));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(n));
            this._del.Delete(new Name("sdfdsfds"));
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