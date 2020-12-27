using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using WorldZero.Service.Entity.Update.Primary;
using NUnit.Framework;

// NOTE: This file does not abide by the limit on a line's character count.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestTaskDel
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

        private PraxisParticipantDel _ppDel;
        private RAMVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMTaskRepo _taskRepo;
        private ITaskTagRepo _taskTagRepo;
        private TaskTagDel _taskTagDel;
        private ITaskFlagRepo _taskFlagRepo;
        private TaskFlagDel _taskFlagDel;
        private IPraxisParticipantRepo _ppRepo;
        private IPraxisRepo _praxisRepo;
        private ICommentRepo _commentRepo;
        private CommentDel _commentDel;
        private IPraxisTagRepo _praxisTagRepo;
        private PraxisTagDel _praxisTagDel;
        private IPraxisFlagRepo _praxisFlagRepo;
        private PraxisFlagDel _praxisFlagDel;
        private PraxisDel _praxisDel;
        private TaskDel _del;
        private RAMStatusRepo _statusRepo;
        private RAMMetaTaskRepo _mtRepo;
        private PraxisUpdate _praxisUpdate;

        private UnsafeFaction _faction0;
        private UnsafeFaction _faction1;
        private UnsafeTask _t0;
        private UnsafeTask _t1;
        private UnsafeTask _t2;
        private UnsafeTaskTag _tTag0_0;
        private UnsafeTaskTag _tTag0_1;
        private UnsafeTaskTag _tTag1_0;
        private UnsafeTaskFlag _tFlag0_0;
        private UnsafeTaskFlag _tFlag1_0;
        private UnsafeTaskFlag _tFlag1_1;
        private UnsafePraxis _praxis0_0;
        private UnsafePraxis _praxis0_1;
        private UnsafePraxis _praxis1_0;
        private UnsafePraxisParticipant _pp0_0;
        private UnsafePraxisParticipant _pp0_1;
        private UnsafePraxisParticipant _pp1_0;

        [SetUp]
        public void Setup()
        {
            var status = new Name("valid");
            var pt = new PointTotal(2);
            var level = new Level(1);
            this._faction0 = new UnsafeFaction(new Name("a"));
            this._faction1 = new UnsafeFaction(new Name("b"));
            this._t0 = new UnsafeTask(this._faction0.Id, status, "x", pt, level);
            this._t1 = new UnsafeTask(this._faction0.Id, status, "x", pt, level);
            this._t2 = new UnsafeTask(this._faction1.Id, status, "x", pt, level);

            this._taskRepo = new RAMTaskRepo();
            this._taskRepo.Insert(this._t0);
            this._taskRepo.Insert(this._t1);
            this._taskRepo.Insert(this._t2);
            this._taskRepo.Save();

            this._taskTagRepo = new RAMTaskTagRepo();
            this._taskTagDel = new TaskTagDel(this._taskTagRepo);
            this._tTag0_0 = new UnsafeTaskTag(this._t0.Id, new Name("x"));
            this._tTag0_1 = new UnsafeTaskTag(this._t0.Id, new Name("y"));
            this._tTag1_0 = new UnsafeTaskTag(this._t1.Id, new Name("x"));
            this._taskTagRepo.Insert(this._tTag0_0);
            this._taskTagRepo.Insert(this._tTag0_1);
            this._taskTagRepo.Insert(this._tTag1_0);
            this._taskTagRepo.Save();

            this._taskFlagRepo = new RAMTaskFlagRepo();
            this._taskFlagDel = new TaskFlagDel(this._taskFlagRepo);
            this._tFlag0_0 = new UnsafeTaskFlag(this._t0.Id, new Name("x"));
            this._tFlag1_0 = new UnsafeTaskFlag(this._t1.Id, new Name("x"));
            this._tFlag1_1 = new UnsafeTaskFlag(this._t1.Id, new Name("y"));
            this._taskFlagRepo.Insert(this._tFlag0_0);
            this._taskFlagRepo.Insert(this._tFlag1_0);
            this._taskFlagRepo.Insert(this._tFlag1_1);
            this._taskFlagRepo.Save();

            // We don't care about using these repos, we just need them for
            // PraxisDel.
            this._commentRepo = new RAMCommentRepo();
            var cfRepo = new RAMCommentFlagRepo();
            var cfDel = new CommentFlagDel(cfRepo);
            this._commentDel = new CommentDel(this._commentRepo, cfDel);
            this._praxisTagRepo = new RAMPraxisTagRepo();
            this._praxisTagDel = new PraxisTagDel(this._praxisTagRepo);
            this._praxisFlagRepo = new RAMPraxisFlagRepo();
            this._praxisFlagDel = new PraxisFlagDel(this._praxisFlagRepo);

            this._praxisRepo = new RAMPraxisRepo();
            this._praxis0_0 = new UnsafePraxis(this._t0.Id, pt, status);
            this._praxis0_1 = new UnsafePraxis(this._t0.Id, pt, status);
            this._praxis1_0 = new UnsafePraxis(this._t1.Id, pt, status);
            this._praxisRepo.Insert(this._praxis0_0);
            this._praxisRepo.Insert(this._praxis0_1);
            this._praxisRepo.Insert(this._praxis1_0);
            this._praxisRepo.Save();

            this._ppRepo = new RAMPraxisParticipantRepo();
            this._pp0_0=new UnsafePraxisParticipant(this._praxis0_0.Id, this._next());
            this._pp0_1=new UnsafePraxisParticipant(this._praxis0_0.Id, this._next());
            this._pp1_0=new UnsafePraxisParticipant(this._praxis1_0.Id, this._next());
            this._ppRepo.Insert(this._pp0_0);
            this._ppRepo.Insert(this._pp1_0);
            this._ppRepo.Save();

            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._statusRepo = new RAMStatusRepo();
            this._mtRepo = new RAMMetaTaskRepo();
            this._praxisUpdate = new PraxisUpdate(
                this._praxisRepo,
                this._ppRepo,
                this._statusRepo,
                this._mtRepo
            );
            this._ppDel = new PraxisParticipantDel(
                this._ppRepo,
                this._praxisRepo,
                this._praxisUpdate,
                this._voteDel
            );
            this._praxisDel = new PraxisDel(
                this._praxisRepo,
                this._ppDel,
                this._commentDel,
                this._praxisTagDel,
                this._praxisFlagDel
            );

            this._del = new TaskDel(
                this._taskRepo,
                this._taskTagDel,
                this._taskFlagDel,
                this._praxisDel
            );
        }

        [TearDown]
        public void TearDown()
        {
            if (this._taskRepo.IsTransactionActive())
            {
                this._taskRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._taskRepo.CleanAll();
        }

        [Test]
        public void TestDeleteSad()
        {
            UnsafeTask t = null;
            Id id = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(t));
            Assert.Throws<ArgumentNullException>(()=>this._del.Delete(id));
            this._del.Delete(new Id(10000));
        }

        [Test]
        public void TestDelete_t0()
        {
            this._del.Delete(this._t0);
            this._absentt<ITaskTag, Id, int>(this._tTag0_0, this._taskTagRepo.GetById);
            this._absentt<ITaskTag, Id, int>(this._tTag0_1, this._taskTagRepo.GetById);
            this._present<ITaskTag, Id, int>(this._tTag1_0, this._taskTagRepo.GetById);
            this._absentt<ITaskFlag, Id, int>(this._tFlag0_0, this._taskFlagRepo.GetById);
            this._present<ITaskFlag, Id, int>(this._tFlag1_0, this._taskFlagRepo.GetById);
            this._present<ITaskFlag, Id, int>(this._tFlag1_1, this._taskFlagRepo.GetById);
            this._absentt<IPraxis, Id, int>(this._praxis0_0, this._praxisRepo.GetById);
            this._absentt<IPraxis, Id, int>(this._praxis0_1, this._praxisRepo.GetById);
            this._present<IPraxis, Id, int>(this._praxis1_0, this._praxisRepo.GetById);
            this._absentt<IPraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<IPraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDelete_t1()
        {
            this._del.Delete(this._t1);
            this._present<ITaskTag, Id, int>(this._tTag0_0, this._taskTagRepo.GetById);
            this._present<ITaskTag, Id, int>(this._tTag0_1, this._taskTagRepo.GetById);
            this._absentt<ITaskTag, Id, int>(this._tTag1_0, this._taskTagRepo.GetById);
            this._present<ITaskFlag, Id, int>(this._tFlag0_0, this._taskFlagRepo.GetById);
            this._absentt<ITaskFlag, Id, int>(this._tFlag1_0, this._taskFlagRepo.GetById);
            this._absentt<ITaskFlag, Id, int>(this._tFlag1_1, this._taskFlagRepo.GetById);
            this._present<IPraxis, Id, int>(this._praxis0_0, this._praxisRepo.GetById);
            this._present<IPraxis, Id, int>(this._praxis0_1, this._praxisRepo.GetById);
            this._absentt<IPraxis, Id, int>(this._praxis1_0, this._praxisRepo.GetById);
            this._present<IPraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<IPraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDeleteNoAssociatedPraxises()
        {
            this._praxisDel.DeleteByTask(this._t0);
            this._absentt<IPraxis, Id, int>(this._praxis0_0, this._praxisRepo.GetById);
            this._absentt<IPraxis, Id, int>(this._praxis0_1, this._praxisRepo.GetById);
            this._absentt<IPraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);

            this._del.Delete(this._t0);
            this._absentt<ITaskTag, Id, int>(this._tTag0_0, this._taskTagRepo.GetById);
            this._absentt<ITaskTag, Id, int>(this._tTag0_1, this._taskTagRepo.GetById);
            this._present<ITaskTag, Id, int>(this._tTag1_0, this._taskTagRepo.GetById);
            this._absentt<ITaskFlag, Id, int>(this._tFlag0_0, this._taskFlagRepo.GetById);
            this._present<ITaskFlag, Id, int>(this._tFlag1_0, this._taskFlagRepo.GetById);
            this._present<ITaskFlag, Id, int>(this._tFlag1_1, this._taskFlagRepo.GetById);
            this._present<IPraxis, Id, int>(this._praxis1_0, this._praxisRepo.GetById);
            this._present<IPraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDeleteByFactionSad()
        {
            Name name = null;
            IFaction faction = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.DeleteByFaction(name));
            Assert.Throws<ArgumentNullException>(()=>this._del.DeleteByFaction(faction));

            this._del.DeleteByFaction(new Name("faaaaakeeee 13"));
        }

        [Test]
        public void TestDeleteByFaction_faction0()
        {
            this._del.DeleteByFaction(this._faction0);
            this._absentt<ITask, Id, int>(this._t0, this._taskRepo.GetById);
            this._absentt<ITask, Id, int>(this._t1, this._taskRepo.GetById);
            this._present<ITask, Id, int>(this._t2, this._taskRepo.GetById);
        }

        [Test]
        public void TestDeleteByFaction_faction1()
        {
            this._del.DeleteByFaction(this._faction1);
            this._present<ITask, Id, int>(this._t0, this._taskRepo.GetById);
            this._present<ITask, Id, int>(this._t1, this._taskRepo.GetById);
            this._absentt<ITask, Id, int>(this._t2, this._taskRepo.GetById);
        }

        [Test]
        public void TestConstructor()
        {
            this._del = new TaskDel(
                this._taskRepo,
                this._taskTagDel,
                this._taskFlagDel,
                this._praxisDel
            );
            Assert.Throws<ArgumentNullException>(()=>new TaskDel(
                null,
                null,
                null,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new TaskDel(
                null,
                this._taskTagDel,
                this._taskFlagDel,
                this._praxisDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new TaskDel(
                this._taskRepo,
                null,
                this._taskFlagDel,
                this._praxisDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new TaskDel(
                this._taskRepo,
                this._taskTagDel,
                null,
                this._praxisDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new TaskDel(
                this._taskRepo,
                this._taskTagDel,
                this._taskFlagDel,
                null
            ));
        }
    }
}