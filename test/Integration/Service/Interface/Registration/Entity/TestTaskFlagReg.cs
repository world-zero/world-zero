using System;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Registration.Entity;
using WorldZero.Service.Registration.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity.Relation
{
    [TestFixture]
    public class TestTaskFlagReg
    {
        private ITaskFlagRepo _tfRepo;
        private ITaskRepo _taskRepo;
        private IFlagRepo _flagRepo;
        private TaskFlagReg _reg;
        private Flag _f;
        private PointTotal _pt;
        private Task _t;

        [SetUp]
        public void Setup()
        {
            this._tfRepo = new RAMTaskFlagRepo();
            this._taskRepo = new RAMTaskRepo();
            this._flagRepo = new RAMFlagRepo();
            this._reg = new TaskFlagReg(
                this._tfRepo,
                this._taskRepo,
                this._flagRepo
            );

            this._f =
                new Flag(new Name("bad"), null, new PointTotal(0.1), false);
            this._flagRepo.Insert(this._f);
            this._flagRepo.Save();

            this._pt = new PointTotal(100);
            this._t = new Task(
                new Name("faction"),
                StatusReg.Active.Id,
                "summary",
                this._pt,
                new Level(1)
            );
            this._taskRepo.Insert(this._t);
            this._taskRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._tfRepo.IsTransactionActive())
            {
                this._tfRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._tfRepo.CleanAll();
        }

        [Test]
        public void TestRegisterSad()
        {
            Assert.Throws<ArgumentNullException>(()=>this._reg.Register(null));
            var tf = new TaskFlag(new Id(100), this._f.Id);
            Assert.Throws<ArgumentException>(()=>this._reg.Register(tf));
            tf = new TaskFlag(this._t.Id, new Name("fake"));
            Assert.Throws<ArgumentException>(()=>this._reg.Register(tf));
        }

        [Test]
        public void TestReigsterHappy()
        {
            var tf = new TaskFlag(this._t.Id, this._f.Id);
            Assert.IsFalse(tf.IsIdSet());
            this._reg.Register(tf);
            Assert.IsTrue(tf.IsIdSet());
            var newT = this._taskRepo.GetById(this._t.Id);
            Assert.AreNotEqual(this._pt, newT.Points);
            Assert.AreEqual(new PointTotal(90), newT.Points);
        }
    }
}