using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

// NOTE: This file does not abide by the limit on a line's character count.

// NOTE: This does not test `Unset()` as that is done by Location.

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestFactionUnset
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

        private RAMUnsafePraxisParticipantRepo _ppRepo;
        private PraxisParticipantDel _ppDel;
        private RAMUnsafeVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMUnsafeFactionRepo _factionRepo;
        private RAMUnsafeMetaTaskRepo _mtRepo;
        private RAMUnsafeCharacterRepo _charRepo;
        private RAMUnsafePraxisRepo _praxisRepo;
        private RAMUnsafeTaskRepo _taskRepo;
        private MetaTaskUnset _mtUnset;
        private RAMUnsafeTaskTagRepo _taskTagRepo;
        private TaskTagDel _taskTagDel;
        private RAMUnsafeTaskFlagRepo _taskFlagRepo;
        private TaskFlagDel _taskFlagDel;
        private RAMUnsafePraxisTagRepo _praxisTagRepo;
        private PraxisTagDel _praxisTagDel;
        private RAMUnsafePraxisFlagRepo _praxisFlagRepo;
        private PraxisFlagDel _praxisFlagDel;
        private RAMUnsafeCommentRepo _commentRepo;
        private CommentDel _commentDel;
        private PraxisDel _praxisDel;
        private TaskDel _taskDel;
        private FactionUnset _unset;
        private UnsafeFaction _faction0;
        private UnsafeFaction _faction1;
        private UnsafeMetaTask _mt0_0;
        private UnsafeMetaTask _mt0_1;
        private UnsafeMetaTask _mt1_0;
        private UnsafeTask _t0_0;
        private UnsafeTask _t1_0;
        private UnsafeTask _t1_1;

        [SetUp]
        public void Setup()
        {
            this._factionRepo = new RAMUnsafeFactionRepo();
            this._mtRepo = new RAMUnsafeMetaTaskRepo();
            this._praxisRepo = new RAMUnsafePraxisRepo();
            this._charRepo = new RAMUnsafeCharacterRepo();
            this._taskRepo = new RAMUnsafeTaskRepo();
            this._mtUnset = new MetaTaskUnset(
                this._mtRepo,
                this._praxisRepo
            );
            this._praxisTagRepo = new RAMUnsafePraxisTagRepo();
            this._praxisTagDel = new PraxisTagDel(this._praxisTagRepo);
            this._praxisFlagRepo = new RAMUnsafePraxisFlagRepo();
            this._praxisFlagDel = new PraxisFlagDel(this._praxisFlagRepo);
            this._commentRepo = new RAMUnsafeCommentRepo();
            this._commentDel = new CommentDel(this._commentRepo);
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
            this._taskTagRepo = new RAMUnsafeTaskTagRepo();
            this._taskTagDel = new TaskTagDel(this._taskTagRepo);
            this._taskFlagRepo = new RAMUnsafeTaskFlagRepo();
            this._taskFlagDel = new TaskFlagDel(this._taskFlagRepo);
            this._taskDel = new TaskDel(
                this._taskRepo,
                this._taskTagDel,
                this._taskFlagDel,
                this._praxisDel
            );
            this._unset = new FactionUnset(
                this._factionRepo,
                this._charRepo,
                this._taskDel,
                this._mtUnset
            );

            var s = new Name("status");
            var pt = new PointTotal(2);
            var level = new Level(2);

            this._faction0 = new UnsafeFaction(new Name("0"));
            this._faction1 = new UnsafeFaction(new Name("1"));
            this._factionRepo.Insert(this._faction0);
            this._factionRepo.Insert(this._faction1);
            this._factionRepo.Save();

            this._mt0_0 = new UnsafeMetaTask(this._faction0.Id, s, "x", pt);
            this._mt0_1 = new UnsafeMetaTask(this._faction0.Id, s, "x", pt);
            this._mt1_0 = new UnsafeMetaTask(this._faction1.Id, s, "x", pt);
            this._mtRepo.Insert(this._mt0_0);
            this._mtRepo.Insert(this._mt0_1);
            this._mtRepo.Insert(this._mt1_0);
            this._mtRepo.Save();

            this._t0_0 = new UnsafeTask(this._faction0.Id, s, "x", pt, level);
            this._t1_0 = new UnsafeTask(this._faction1.Id, s, "x", pt, level);
            this._t1_1 = new UnsafeTask(this._faction1.Id, s, "x", pt, level);
            this._taskRepo.Insert(this._t0_0);
            this._taskRepo.Insert(this._t1_0);
            this._taskRepo.Insert(this._t1_1);
            this._taskRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._mtRepo.IsTransactionActive())
            {
                this._mtRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._mtRepo.CleanAll();
        }


        [Test]
        public void TestDeleteSad()
        {
            Name name = null;
            UnsafeFaction faction = null;
            Assert.Throws<ArgumentNullException>(()=>this._unset.Delete(name));
            Assert.Throws<ArgumentNullException>(()=>this._unset.Delete(faction));

            this._unset.Delete(new Name("faaaaakeeee 13"));
        }

        [Test]
        public void TestDelete_faction0()
        {
            this._unset.Delete(this._faction0);
            this._absentt<UnsafeMetaTask, Id, int>(this._mt0_0, this._mtRepo.GetById);
            this._absentt<UnsafeMetaTask, Id, int>(this._mt0_1, this._mtRepo.GetById);
            this._present<UnsafeMetaTask, Id, int>(this._mt1_0, this._mtRepo.GetById);
            this._absentt<UnsafeTask, Id, int>(this._t0_0, this._taskRepo.GetById);
            this._present<UnsafeTask, Id, int>(this._t1_0, this._taskRepo.GetById);
            this._present<UnsafeTask, Id, int>(this._t1_1, this._taskRepo.GetById);
        }

        [Test]
        public void TestDelete_faction1()
        {
            this._unset.Delete(this._faction1);
            this._present<UnsafeMetaTask, Id, int>(this._mt0_0, this._mtRepo.GetById);
            this._present<UnsafeMetaTask, Id, int>(this._mt0_1, this._mtRepo.GetById);
            this._absentt<UnsafeMetaTask, Id, int>(this._mt1_0, this._mtRepo.GetById);
            this._present<UnsafeTask, Id, int>(this._t0_0, this._taskRepo.GetById);
            this._absentt<UnsafeTask, Id, int>(this._t1_0, this._taskRepo.GetById);
            this._absentt<UnsafeTask, Id, int>(this._t1_1, this._taskRepo.GetById);
        }

        [Test]
        public void TestConstructor()
        {
            new FactionUnset(
                this._factionRepo,
                this._charRepo,
                this._taskDel,
                this._mtUnset
            );
            Assert.Throws<ArgumentNullException>(()=>new FactionUnset(
                null,
                this._charRepo,
                this._taskDel,
                this._mtUnset
            ));
            Assert.Throws<ArgumentNullException>(()=>new FactionUnset(
                this._factionRepo,
                null,
                this._taskDel,
                this._mtUnset
            ));
            Assert.Throws<ArgumentNullException>(()=>new FactionUnset(
                this._factionRepo,
                this._charRepo,
                this._taskDel,
                null
            ));
        }
    }
}