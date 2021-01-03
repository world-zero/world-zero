using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;
using WorldZero.Service.Constant.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity.Update.Primary
{
    [TestFixture]
    public class TestABCIdStatusedEntityUpdate
    {
        private RAMStatusRepo _statusRepo;
        private Name _oldStatus;
        private Name _newStatus;
        private Name _newStatusStatic;
        private ITask _t;
        private RAMTaskRepo _repo;
        private TestIdStatusedEntityUpdate _update;

        [SetUp]
        public void Setup()
        {
            this._statusRepo = new RAMStatusRepo();
            this._oldStatus = new Name("Old");
            this._newStatus = new Name("repo-only status");
            var s = new UnsafeStatus(this._oldStatus);
            this._statusRepo.Insert(s);
            s = new UnsafeStatus(this._newStatus);
            this._statusRepo.Insert(s);
            this._statusRepo.Save();
            this._newStatusStatic = ConstantStatuses.Active.Id;
            var pt = new PointTotal(2);
            var level = new Level(2);
            this._t = new UnsafeTask(new Name("s"), this._oldStatus, "x", pt, level);
            this._repo = new RAMTaskRepo();
            this._repo.Insert(this._t);
            this._repo.Save();
            this._update = new TestIdStatusedEntityUpdate(this._repo, this._statusRepo);
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
        public void TestAmendStatusHappy()
        {
            Assert.AreEqual(this._t.StatusId, this._oldStatus);

            this._update.AmendStatus(this._t.Id, this._newStatusStatic);
            this._t = this._repo.GetById(this._t.Id);
            Assert.AreEqual(this._t.StatusId, this._newStatusStatic);

            this._update.AmendStatus(this._t.Id, this._newStatus);
            this._t = this._repo.GetById(this._t.Id);
            Assert.AreEqual(this._t.StatusId, this._newStatus);

            this._update.AmendStatus(this._t.Id, ConstantStatuses.Active);
            this._t = this._repo.GetById(this._t.Id);
            Assert.AreEqual(this._t.StatusId, ConstantStatuses.Active.Id);
        }

        [Test]
        public void TestAmendStatusSad()
        {
            ITask t = null;
            Id id = null;
            IStatus s = null;
            Name name = null;

            // bad e, good status
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                t, ConstantStatuses.Active));

            // good e, bad status
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                this._t, s));

            // bad id, good status
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                id, ConstantStatuses.Active));

            // good id, bad status
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                this._t.Id, s));

            // bad e, good name
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                t, ConstantStatuses.Active.Id));

            // good e, bad name
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                this._t, name));

            // bad id, good name
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                id, ConstantStatuses.Active.Id));

            // good id, bad name
            Assert.Throws<ArgumentNullException>(()=>this._update.AmendStatus(
                this._t.Id, name));
        }
    }

    public class TestIdStatusedEntityUpdate : ABCIdStatusedEntityUpdate<ITask>
    {
        public
        TestIdStatusedEntityUpdate(ITaskRepo repo, IStatusRepo statusRepo)
            : base(repo, statusRepo)
        { }
    }
}