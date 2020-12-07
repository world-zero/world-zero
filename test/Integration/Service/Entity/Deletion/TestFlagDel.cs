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
    public class TestFlagDel
    {
        private UnsafeFlag _f0;
        private UnsafeFlag _t1;
        private RAMUnsafeFlagRepo _flagRepo;
        private RAMTaskFlagRepo _taskFlagRepo;
        private RAMMetaTaskFlagRepo _mtFlagRepo;
        private RAMPraxisFlagRepo _praxisFlagRepo;
        private TaskFlagDel _taskFlagDel;
        private MetaTaskFlagDel _mtFlagDel;
        private PraxisFlagDel _praxisFlagDel;
        private FlagDel _del;

        [SetUp]
        public void Setup()
        {
            this._f0 = new UnsafeFlag(new Name("#valid"));
            this._t1 = new UnsafeFlag(new Name("#invalid"));

            this._flagRepo = new RAMUnsafeFlagRepo();
            this._flagRepo.Insert(this._f0);
            this._flagRepo.Insert(this._t1);
            this._flagRepo.Save();

            this._taskFlagRepo = new RAMTaskFlagRepo();
            this._taskFlagRepo.Insert(new TaskFlag(new Id(100), this._f0.Id));
            this._taskFlagRepo.Insert(new TaskFlag(new Id(200), this._f0.Id));
            this._taskFlagRepo.Insert(new TaskFlag(new Id(300), this._t1.Id));
            this._taskFlagRepo.Save();

            this._mtFlagRepo = new RAMMetaTaskFlagRepo();
            this._mtFlagRepo.Insert(new MetaTaskFlag(new Id(400), this._f0.Id));
            this._mtFlagRepo.Insert(new MetaTaskFlag(new Id(500), this._t1.Id));
            this._mtFlagRepo.Insert(new MetaTaskFlag(new Id(600), this._t1.Id));
            this._mtFlagRepo.Save();

            this._praxisFlagRepo = new RAMPraxisFlagRepo();
            this._praxisFlagRepo.Insert(new PraxisFlag(new Id(70), this._f0.Id));
            this._praxisFlagRepo.Insert(new PraxisFlag(new Id(80), this._t1.Id));
            this._praxisFlagRepo.Insert(new PraxisFlag(new Id(90), this._t1.Id));
            this._praxisFlagRepo.Save();

            this._taskFlagDel = new TaskFlagDel(this._taskFlagRepo);
            this._mtFlagDel = new MetaTaskFlagDel(this._mtFlagRepo);
            this._praxisFlagDel = new PraxisFlagDel(this._praxisFlagRepo);

            this._del = new FlagDel(
                this._flagRepo,
                this._taskFlagDel,
                this._mtFlagDel,
                this._praxisFlagDel
            );
        }

        [TearDown]
        public void TearDown()
        {
            if (this._flagRepo.IsTransactionActive())
            {
                this._flagRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._flagRepo.CleanAll();
        }

        [Test]
        public void TestDeleteSad()
        {
            UnsafeFlag t = null;
            Name n = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(t));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(n));
            this._del.Delete(new Name("fdsfdff"));
        }

        [Test]
        public void TestDelete()
        {
            Assert.AreEqual(2,
                this._taskFlagRepo.GetByRightId(this._f0.Id).Count());
            Assert.AreEqual(1,
                this._taskFlagRepo.GetByRightId(this._t1.Id).Count());
            Assert.AreEqual(1,
                this._mtFlagRepo.GetByRightId(this._f0.Id).Count());
            Assert.AreEqual(2,
                this._mtFlagRepo.GetByRightId(this._t1.Id).Count());
            Assert.AreEqual(1,
                this._praxisFlagRepo.GetByRightId(this._f0.Id).Count());
            Assert.AreEqual(2,
                this._praxisFlagRepo.GetByRightId(this._t1.Id).Count());

            this._del.Delete(this._f0);

            Assert.Throws<ArgumentException>(()=>
                this._flagRepo.GetById(this._f0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._taskFlagRepo.GetByRightId(this._f0.Id).Count());
            Assert.AreEqual(1,
                this._taskFlagRepo.GetByRightId(this._t1.Id).Count());
            Assert.Throws<ArgumentException>(()=>
                this._mtFlagRepo.GetByRightId(this._f0.Id).Count());
            Assert.AreEqual(2,
                this._mtFlagRepo.GetByRightId(this._t1.Id).Count());
            Assert.Throws<ArgumentException>(()=>
                this._praxisFlagRepo.GetByRightId(this._f0.Id).Count());
            Assert.AreEqual(2,
                this._praxisFlagRepo.GetByRightId(this._t1.Id).Count());
        }
    }
}