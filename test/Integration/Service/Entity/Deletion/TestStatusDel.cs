using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.General.Generic;
using NUnit.Framework;

// NOTE: This file does not abide by the limit on a line's character count.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestStatusDel
    {
        private int _nxt = 10000;
        private Id _next() => new Id(this._nxt++);

        private void _absentt<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> getById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            Assert.Throws<ArgumentException>(()=>getById(e.Id));
        }

        private void _present<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> GetById)
            where TEntity : IEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            var actualEntity = GetById(e.Id);
            Assert.AreEqual(actualEntity.Id, e.Id);
        }

        private IStatusRepo _statusRepo;
        private ITaskRepo _taskRepo;
        private TaskDel _taskDel;
        private ICommentRepo _commentRepo;
        private CommentDel _commentDel;
        private IPraxisTagRepo _praxisTagRepo;
        private PraxisTagDel _praxisTagDel;
        private IPraxisFlagRepo _praxisFlagRepo;
        private PraxisFlagDel _praxisFlagDel;
        private IVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private IPraxisRepo _praxisRepo;
        private IPraxisParticipantRepo _ppRepo;
        private PraxisDel _praxisDel;
        private ITaskTagRepo _taskTagRepo;
        private TaskTagDel _taskTagDel;
        private ITaskFlagRepo _taskFlagRepo;
        private TaskFlagDel _taskFlagDel;
        private IMetaTaskRepo _mtRepo;
        private MetaTaskUnset _mtUnset;
        private StatusDel _statusDel;

        private Status _s0;
        private Status _s1;
        private Task _t0;
        private Task _t1;
        private MetaTask _mt0;
        private MetaTask _mt1;
        private Praxis _praxis0;
        private Praxis _praxis1;

        [SetUp]
        public void Setup()
        {
            this._statusRepo = new RAMStatusRepo();
            this._commentRepo = new RAMCommentRepo();
            this._commentDel = new CommentDel(this._commentRepo);
            this._praxisTagRepo = new RAMPraxisTagRepo();
            this._praxisTagDel = new PraxisTagDel(this._praxisTagRepo);
            this._praxisFlagRepo = new RAMPraxisFlagRepo();
            this._praxisFlagDel = new PraxisFlagDel(this._praxisFlagRepo);
            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._praxisRepo = new RAMPraxisRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._praxisDel = new PraxisDel(
                this._praxisRepo,
                this._ppRepo,
                this._commentDel,
                this._voteDel,
                this._praxisTagDel,
                this._praxisFlagDel
            );
            this._taskRepo = new RAMTaskRepo();
            this._taskTagRepo = new RAMTaskTagRepo();
            this._taskTagDel = new TaskTagDel(this._taskTagRepo);
            this._taskFlagRepo = new RAMTaskFlagRepo();
            this._taskFlagDel = new TaskFlagDel(this._taskFlagRepo);
            this._taskDel = new TaskDel(
                this._taskRepo,
                this._taskTagDel,
                this._taskFlagDel,
                this._praxisDel
            );
            this._mtRepo = new RAMMetaTaskRepo();
            this._mtUnset = new MetaTaskUnset(
                this._mtRepo,
                this._praxisRepo
            );
            this._statusDel = new StatusDel(
                this._statusRepo,
                this._praxisDel,
                this._taskDel,
                this._mtUnset
            );

            var pt = new PointTotal(2);
            this._s0 = new Status(new Name("0"));
            this._s1 = new Status(new Name("1"));
            this._statusRepo.Insert(this._s0);
            this._statusRepo.Insert(this._s1);
            this._statusRepo.Save();

            var f = new Name("faction");
            var level = new Level(2);
            this._t0 = new Task(f, this._s0.Id, "x", pt, level);
            this._t1 = new Task(f, this._s1.Id, "x", pt, level);
            this._taskRepo.Insert(this._t0);
            this._taskRepo.Insert(this._t1);
            this._taskRepo.Save();

            this._mt0 = new MetaTask(f, this._s0.Id, "x", pt);
            this._mt1 = new MetaTask(f, this._s1.Id, "x", pt);
            this._mtRepo.Insert(this._mt0);
            this._mtRepo.Insert(this._mt1);
            this._mtRepo.Save();

            this._praxis0 = new Praxis(this._next(), pt, this._s0.Id);
            this._praxis1 = new Praxis(this._next(), pt, this._s1.Id);
            this._praxisRepo.Insert(this._praxis0);
            this._praxisRepo.Insert(this._praxis1);
            this._praxisRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._statusRepo.IsTransactionActive())
            {
                this._statusRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._statusRepo.CleanAll();
        }

        [Test]
        public void TestDeleteSad()
        {
            Name name = null;
            Status s = null;
            Assert.Throws<ArgumentNullException>(()=>this._statusDel.Delete(name));
            Assert.Throws<ArgumentNullException>(()=>this._statusDel.Delete(s));
            this._statusDel.Delete(new Name("faaaaakeeee"));
        }

        [Test]
        public void TestDelete()
        {
            this._statusDel.Delete(this._s0);
            this._absentt<Status, Name, string>(this._s0, this._statusRepo.GetById);
            this._present<Status, Name, string>(this._s1, this._statusRepo.GetById);
            this._absentt<Task, Id, int>(this._t0, this._taskRepo.GetById);
            this._present<Task, Id, int>(this._t1, this._taskRepo.GetById);
            this._absentt<MetaTask, Id, int>(this._mt0, this._mtRepo.GetById);
            this._present<MetaTask, Id, int>(this._mt1, this._mtRepo.GetById);
            this._absentt<Praxis, Id, int>(this._praxis0, this._praxisRepo.GetById);
            this._present<Praxis, Id, int>(this._praxis1, this._praxisRepo.GetById);
            this._statusDel.Delete(this._s0);
        }

        [Test]
        public void TestDeleteNoRelatedPraxises()
        {
            this._praxisDel.Delete(this._praxis0);
            this._praxisDel.Delete(this._praxis1);
            this._statusDel.Delete(this._s0);
            this._absentt<Status, Name, string>(this._s0, this._statusRepo.GetById);
            this._present<Status, Name, string>(this._s1, this._statusRepo.GetById);
            this._absentt<Task, Id, int>(this._t0, this._taskRepo.GetById);
            this._present<Task, Id, int>(this._t1, this._taskRepo.GetById);
            this._absentt<MetaTask, Id, int>(this._mt0, this._mtRepo.GetById);
            this._present<MetaTask, Id, int>(this._mt1, this._mtRepo.GetById);
            this._absentt<Praxis, Id, int>(this._praxis0, this._praxisRepo.GetById);
            this._absentt<Praxis, Id, int>(this._praxis1, this._praxisRepo.GetById);
            this._statusDel.Delete(this._s0);
        }

        [Test]
        public void TestDeleteNoRelatedTasks()
        {
            this._taskDel.Delete(this._t0);
            this._taskDel.Delete(this._t1);
            this._statusDel.Delete(this._s0);
            this._absentt<Status, Name, string>(this._s0, this._statusRepo.GetById);
            this._present<Status, Name, string>(this._s1, this._statusRepo.GetById);
            this._absentt<Task, Id, int>(this._t0, this._taskRepo.GetById);
            this._absentt<Task, Id, int>(this._t1, this._taskRepo.GetById);
            this._absentt<MetaTask, Id, int>(this._mt0, this._mtRepo.GetById);
            this._present<MetaTask, Id, int>(this._mt1, this._mtRepo.GetById);
            this._absentt<Praxis, Id, int>(this._praxis0, this._praxisRepo.GetById);
            this._present<Praxis, Id, int>(this._praxis1, this._praxisRepo.GetById);
            this._statusDel.Delete(this._s0);
        }

        [Test]
        public void TestDeleteNoRelatedMetaTasks()
        {
            this._mtUnset.Delete(this._mt0);
            this._mtUnset.Delete(this._mt1);
            this._statusDel.Delete(this._s0);
            this._absentt<Status, Name, string>(this._s0, this._statusRepo.GetById);
            this._present<Status, Name, string>(this._s1, this._statusRepo.GetById);
            this._absentt<Task, Id, int>(this._t0, this._taskRepo.GetById);
            this._present<Task, Id, int>(this._t1, this._taskRepo.GetById);
            this._absentt<MetaTask, Id, int>(this._mt0, this._mtRepo.GetById);
            this._absentt<MetaTask, Id, int>(this._mt1, this._mtRepo.GetById);
            this._absentt<Praxis, Id, int>(this._praxis0, this._praxisRepo.GetById);
            this._present<Praxis, Id, int>(this._praxis1, this._praxisRepo.GetById);
            this._statusDel.Delete(this._s0);
        }
    }
}