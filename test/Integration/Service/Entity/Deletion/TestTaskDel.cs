using System;
using System.Linq;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
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
            where TEntity : UnsafeIEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            Assert.Throws<ArgumentException>(()=>getById(e.Id));
        }

        private void _present<TEntity, TId, TBuiltIn>(TEntity e, Func<TId, TEntity> GetById)
            where TEntity : UnsafeIEntity<TId, TBuiltIn>
            where TId : ISingleValueObject<TBuiltIn>
        {
            var actualEntity = GetById(e.Id);
            Assert.AreEqual(actualEntity.Id, e.Id);
        }

        private PraxisParticipantDel _ppDel;
        private RAMUnsafeVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMUnsafeTaskRepo _taskRepo;
        private IUnsafeTaskTagRepo _taskTagRepo;
        private TaskTagDel _taskTagDel;
        private IUnsafeTaskFlagRepo _taskFlagRepo;
        private TaskFlagDel _taskFlagDel;
        private IUnsafePraxisParticipantRepo _ppRepo;
        private IUnsafePraxisRepo _praxisRepo;
        private IUnsafeCommentRepo _commentRepo;
        private CommentDel _commentDel;
        private IUnsafePraxisTagRepo _praxisTagRepo;
        private PraxisTagDel _praxisTagDel;
        private IUnsafePraxisFlagRepo _praxisFlagRepo;
        private PraxisFlagDel _praxisFlagDel;
        private PraxisDel _praxisDel;
        private TaskDel _del;

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

            this._taskRepo = new RAMUnsafeTaskRepo();
            this._taskRepo.Insert(this._t0);
            this._taskRepo.Insert(this._t1);
            this._taskRepo.Insert(this._t2);
            this._taskRepo.Save();

            this._taskTagRepo = new RAMUnsafeTaskTagRepo();
            this._taskTagDel = new TaskTagDel(this._taskTagRepo);
            this._tTag0_0 = new UnsafeTaskTag(this._t0.Id, new Name("x"));
            this._tTag0_1 = new UnsafeTaskTag(this._t0.Id, new Name("y"));
            this._tTag1_0 = new UnsafeTaskTag(this._t1.Id, new Name("x"));
            this._taskTagRepo.Insert(this._tTag0_0);
            this._taskTagRepo.Insert(this._tTag0_1);
            this._taskTagRepo.Insert(this._tTag1_0);
            this._taskTagRepo.Save();

            this._taskFlagRepo = new RAMUnsafeTaskFlagRepo();
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
            this._commentRepo = new RAMUnsafeCommentRepo();
            this._commentDel = new CommentDel(this._commentRepo);
            this._praxisTagRepo = new RAMUnsafePraxisTagRepo();
            this._praxisTagDel = new PraxisTagDel(this._praxisTagRepo);
            this._praxisFlagRepo = new RAMUnsafePraxisFlagRepo();
            this._praxisFlagDel = new PraxisFlagDel(this._praxisFlagRepo);

            this._praxisRepo = new RAMUnsafePraxisRepo();
            this._praxis0_0 = new UnsafePraxis(this._t0.Id, pt, status);
            this._praxis0_1 = new UnsafePraxis(this._t0.Id, pt, status);
            this._praxis1_0 = new UnsafePraxis(this._t1.Id, pt, status);
            this._praxisRepo.Insert(this._praxis0_0);
            this._praxisRepo.Insert(this._praxis0_1);
            this._praxisRepo.Insert(this._praxis1_0);
            this._praxisRepo.Save();

            this._ppRepo = new RAMUnsafePraxisParticipantRepo();
            this._pp0_0=new UnsafePraxisParticipant(this._praxis0_0.Id, this._next());
            this._pp0_1=new UnsafePraxisParticipant(this._praxis0_0.Id, this._next());
            this._pp1_0=new UnsafePraxisParticipant(this._praxis1_0.Id, this._next());
            this._ppRepo.Insert(this._pp0_0);
            this._ppRepo.Insert(this._pp1_0);
            this._ppRepo.Save();

            this._voteRepo = new RAMUnsafeVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._ppRepo = new RAMUnsafePraxisParticipantRepo();
            this._ppDel = new PraxisParticipantDel(
                this._ppRepo,
                this._praxisRepo,
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
            this._absentt<UnsafeTaskTag, Id, int>(this._tTag0_0, this._taskTagRepo.GetById);
            this._absentt<UnsafeTaskTag, Id, int>(this._tTag0_1, this._taskTagRepo.GetById);
            this._present<UnsafeTaskTag, Id, int>(this._tTag1_0, this._taskTagRepo.GetById);
            this._absentt<UnsafeTaskFlag, Id, int>(this._tFlag0_0, this._taskFlagRepo.GetById);
            this._present<UnsafeTaskFlag, Id, int>(this._tFlag1_0, this._taskFlagRepo.GetById);
            this._present<UnsafeTaskFlag, Id, int>(this._tFlag1_1, this._taskFlagRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis0_0, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis0_1, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis1_0, this._praxisRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDelete_t1()
        {
            this._del.Delete(this._t1);
            this._present<UnsafeTaskTag, Id, int>(this._tTag0_0, this._taskTagRepo.GetById);
            this._present<UnsafeTaskTag, Id, int>(this._tTag0_1, this._taskTagRepo.GetById);
            this._absentt<UnsafeTaskTag, Id, int>(this._tTag1_0, this._taskTagRepo.GetById);
            this._present<UnsafeTaskFlag, Id, int>(this._tFlag0_0, this._taskFlagRepo.GetById);
            this._absentt<UnsafeTaskFlag, Id, int>(this._tFlag1_0, this._taskFlagRepo.GetById);
            this._absentt<UnsafeTaskFlag, Id, int>(this._tFlag1_1, this._taskFlagRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis0_0, this._praxisRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis0_1, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis1_0, this._praxisRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDeleteNoAssociatedPraxises()
        {
            this._praxisDel.DeleteByTask(this._t0);
            this._absentt<UnsafePraxis, Id, int>(this._praxis0_0, this._praxisRepo.GetById);
            this._absentt<UnsafePraxis, Id, int>(this._praxis0_1, this._praxisRepo.GetById);
            this._absentt<UnsafePraxisParticipant, Id, int>(this._pp0_0, this._ppRepo.GetById);

            this._del.Delete(this._t0);
            this._absentt<UnsafeTaskTag, Id, int>(this._tTag0_0, this._taskTagRepo.GetById);
            this._absentt<UnsafeTaskTag, Id, int>(this._tTag0_1, this._taskTagRepo.GetById);
            this._present<UnsafeTaskTag, Id, int>(this._tTag1_0, this._taskTagRepo.GetById);
            this._absentt<UnsafeTaskFlag, Id, int>(this._tFlag0_0, this._taskFlagRepo.GetById);
            this._present<UnsafeTaskFlag, Id, int>(this._tFlag1_0, this._taskFlagRepo.GetById);
            this._present<UnsafeTaskFlag, Id, int>(this._tFlag1_1, this._taskFlagRepo.GetById);
            this._present<UnsafePraxis, Id, int>(this._praxis1_0, this._praxisRepo.GetById);
            this._present<UnsafePraxisParticipant, Id, int>(this._pp1_0, this._ppRepo.GetById);
        }

        [Test]
        public void TestDeleteByFactionSad()
        {
            Name name = null;
            UnsafeFaction faction = null;
            Assert.Throws<ArgumentNullException>(()=>this._del.DeleteByFaction(name));
            Assert.Throws<ArgumentNullException>(()=>this._del.DeleteByFaction(faction));

            this._del.DeleteByFaction(new Name("faaaaakeeee 13"));
        }

        [Test]
        public void TestDeleteByFaction_faction0()
        {
            this._del.DeleteByFaction(this._faction0);
            this._absentt<UnsafeTask, Id, int>(this._t0, this._taskRepo.GetById);
            this._absentt<UnsafeTask, Id, int>(this._t1, this._taskRepo.GetById);
            this._present<UnsafeTask, Id, int>(this._t2, this._taskRepo.GetById);
        }

        [Test]
        public void TestDeleteByFaction_faction1()
        {
            this._del.DeleteByFaction(this._faction1);
            this._present<UnsafeTask, Id, int>(this._t0, this._taskRepo.GetById);
            this._present<UnsafeTask, Id, int>(this._t1, this._taskRepo.GetById);
            this._absentt<UnsafeTask, Id, int>(this._t2, this._taskRepo.GetById);
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