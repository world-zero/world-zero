using System;
using System.Linq;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using NUnit.Framework;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Repository.Entity.RAM.Primary;

// This breaks the test fixture into two test fixtures because I migrated a
// bunch of operations from IPraxisRepo to IPraxisParticipantRepo and it was
// being annoying to rewrite all of either of the tests to play well so I just
// didn't.

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Relation
{
    [TestFixture]
    public class TestRAMPraxisParticipantRepoFirst
    {
        private RAMPraxisParticipantRepo _repo;
        private Id _pId0;
        private Id _pId1;
        private Id _cId0;
        private Id _cId1;
        private UnsafePraxisParticipant _pp0;
        private UnsafePraxisParticipant _pp1;
        private UnsafePraxisParticipant _ppAlt;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMPraxisParticipantRepo();
            this._pId0 = new Id(1);
            this._pId1 = new Id(2);
            this._cId0 = new Id(3);
            this._cId1 = new Id(4);
            this._pp0 = new UnsafePraxisParticipant(this._pId0, this._cId0);
            this._pp1 = new UnsafePraxisParticipant(this._pId0, this._cId1);
            this._ppAlt = new UnsafePraxisParticipant(this._pId1, this._cId0);
        }

        [TearDown]
        public void TearDown()
        {
            this._repo.CleanAll();
        }

        [Test]
        public void TestGetByPlayerId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.GetCharIdsByPraxisId(null));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetCharIdsByPraxisId(new Id(234)));

            this._repo.Insert(this._pp0);
            this._repo.Save();
            var ids = this._repo.GetCharIdsByPraxisId(this._pId0).ToList();
            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(this._cId0, ids[0]);

            this._repo.Insert(this._pp1);
            this._repo.Save();
            ids = this._repo.GetCharIdsByPraxisId(this._pId0).ToList();
            Assert.AreEqual(2, ids.Count);
            Assert.AreEqual(this._cId0, ids[0]);
            Assert.AreEqual(this._cId1, ids[1]);

            this._repo.Insert(this._ppAlt);
            this._repo.Save();
            ids = this._repo.GetCharIdsByPraxisId(this._pId1).ToList();
            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(this._cId0, ids[0]);
        }

        [Test]
        public void TestParticipantCheck()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.ParticipantCheck(null, new Id(3)));
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.ParticipantCheck(new Id(3), null));

            this._repo.Insert(this._pp0);
            this._repo.Save();

            Assert.IsFalse(this._repo.ParticipantCheck(new Id(22), new Id(9)));
            Assert.IsTrue(this._repo.ParticipantCheck(
                this._pp0.PraxisId, this._pp0.CharacterId));
        }

        [Test]
        public void TestGetParticipantCount()
        {
            Assert.AreEqual(0, this._repo.GetParticipantCountViaPraxisId(this._pId0));

            this._repo.Insert(this._pp0);
            Assert.AreEqual(0, this._repo.GetParticipantCountViaPraxisId(this._pId0));
            this._repo.Save();
            Assert.AreEqual(1, this._repo.GetParticipantCountViaPraxisId(this._pId0));

            this._repo.Insert(this._pp1);
            Assert.AreEqual(1, this._repo.GetParticipantCountViaPraxisId(this._pId0));
            this._repo.Save();
            Assert.AreEqual(2, this._repo.GetParticipantCountViaPraxisId(this._pId0));
        }

        [Test]
        public void TestGetParticipantCountViaPPId()
        {
            this._repo.Insert(this._pp0);
            this._repo.Insert(this._pp1);
            this._repo.Insert(this._ppAlt);
            this._repo.Save();

            Assert.Throws<ArgumentNullException>(()=>
                this._repo.GetParticipantCountViaPPId(null));
            Assert.AreEqual(0,
                this._repo.GetParticipantCountViaPPId(new Id(20000)));
            Assert.AreEqual(2,
                this._repo.GetParticipantCountViaPPId(this._pp0.Id));
            Assert.AreEqual(1,
                this._repo.GetParticipantCountViaPPId(this._ppAlt.Id));
        }

        [Test]
        public void TestGetParticipantCountViaCharId()
        {
            this._repo.Insert(this._pp0);
            this._repo.Insert(this._pp1);
            this._repo.Insert(this._ppAlt);
            this._repo.Save();

            Assert.Throws<ArgumentNullException>(()=>
                this._repo.GetParticipantCountsViaCharId(null).Count());
            Assert.AreEqual(2,
                this._repo.GetParticipantCountsViaCharId(this._cId0).Count());
            Assert.AreEqual(1,
                this._repo.GetParticipantCountsViaCharId(this._cId1).Count());
        }
    }

    [TestFixture]
    public class TestRAMPraxisParticipantRepoSecond
    {
        private RAMPraxisRepo _praxisRepo;
        private TestPraxisParticipantRepoExposedData _ppRepo;
        private HashSet<Name> _goodStatuses;
        private Id _taskId0;
        private Id _taskId1;
        private Name _goodStatus;
        private Name _otherGoodStatus;
        private Name _badStatus;
        private UnsafePraxis _p0;
        private UnsafePraxis _p1;
        private UnsafePraxis _p2;
        private UnsafePraxisParticipant _pp0;
        private UnsafePraxisParticipant _pp1;
        private UnsafePraxisParticipant _pp2;
        private UnsafePraxisParticipant _pp3;
        private Id _charId0;
        private Id _charId1;
        private Id _charIdUnused;
        private PointTotal _points;

        [SetUp]
        public void Setup()
        {
            this._praxisRepo = new RAMPraxisRepo();
            this._ppRepo = new TestPraxisParticipantRepoExposedData();
            this._goodStatus = new Name("good");
            this._otherGoodStatus = new Name("other good");
            this._badStatus = new Name("bad");
            this._goodStatuses = new HashSet<Name>();
            this._goodStatuses.Add(this._goodStatus);
            this._goodStatuses.Add(this._otherGoodStatus);
            this._points = new PointTotal(3);
            this._taskId0 = new Id(1);
            this._taskId1 = new Id(2);
            this._p0 = new UnsafePraxis(this._taskId0,this._points,this._goodStatus);
            this._p1 = new UnsafePraxis(this._taskId0,this._points,this._goodStatus);
            this._p2 = new UnsafePraxis(this._taskId1,this._points,this._badStatus);
            this._praxisRepo.Insert(this._p0);
            this._praxisRepo.Insert(this._p1);
            this._praxisRepo.Save();
            this._charId0 = new Id(1);
            this._charId1 = new Id(2);
            this._charIdUnused = new Id(3);
            this._pp0 = new UnsafePraxisParticipant(this._p0.Id, this._charId0);
            this._pp1 = new UnsafePraxisParticipant(this._p0.Id, this._charId1);
            this._pp2 = new UnsafePraxisParticipant(this._p1.Id, this._charId0);
            this._pp3 = new UnsafePraxisParticipant(this._p2.Id, this._charId0);
            this._ppRepo.Insert(this._pp0);
            this._ppRepo.Insert(this._pp1);
            this._ppRepo.Insert(this._pp2);
            this._ppRepo.Insert(this._pp3);
            this._ppRepo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            this._praxisRepo.CleanAll();
        }

        [Test]
        public void TestGetCharacterSubmissionCountSad()
        {
            Assert.Throws<ArgumentNullException>(()=>this._ppRepo
                .GetCharacterSubmissionCount(null, this._charId0));
            Assert.Throws<ArgumentNullException>(()=>this._ppRepo
                .GetCharacterSubmissionCount(this._taskId0, null));
            Id badId = new Id(100000);
            Assert.AreEqual(0, this._ppRepo
                .GetCharacterSubmissionCount(badId, badId));
            Assert.AreEqual(0, this._ppRepo
                .GetCharacterSubmissionCount(this._taskId1, this._charId1));
            this._ppRepo.Data.Remove(typeof(UnsafePraxis).FullName);
            Assert.AreEqual(0, this._ppRepo
                .GetCharacterSubmissionCount(this._taskId0, this._charId0));
        }

        [Test]
        public void TestGetCharacterSubmissionCountHappy()
        {
            Assert.AreEqual(2, this._ppRepo
                .GetCharacterSubmissionCount(this._taskId0, this._charId0));
        }

        [Test]
        public void TestGetCharacterSubmissionCountViaPraxisIdSad()
        {
            Assert.Throws<ArgumentNullException>(()=>this._ppRepo
                .GetCharacterSubmissionCountViaPraxisId(null, this._charId0));
            Assert.Throws<ArgumentNullException>(()=>this._ppRepo
                .GetCharacterSubmissionCountViaPraxisId(this._p0.Id, null));
            Id badId = new Id(100000);
            Assert.AreEqual(0, this._ppRepo
                .GetCharacterSubmissionCountViaPraxisId(badId, badId));
            Assert.AreEqual(0, this._ppRepo
                .GetCharacterSubmissionCountViaPraxisId(
                    this._p0.Id,
                    this._charIdUnused
                )
            );
            this._ppRepo.Data.Remove(typeof(UnsafePraxis).FullName);
            Assert.AreEqual(0, this._ppRepo
                .GetCharacterSubmissionCountViaPraxisId(
                    this._p0.Id,
                    this._charId0
                )
            );
        }

        [Test]
        public void TestGetCharacterSubmissionCountViaPraxisIdHappy()
        {
            Assert.AreEqual(2, this._ppRepo
                .GetCharacterSubmissionCountViaPraxisId(
                    this._p0.Id,
                    this._charId0
                )
            );
        }

        [Test]
        public void TestGetPraxisCountBadArgs()
        {
            var id = new Id(2);
            var set = new HashSet<Name>();
            set.Add(new Name("x"));
            Assert.Throws<ArgumentNullException>(()=>
                this._ppRepo.GetPraxisCount(null, set));
            Assert.Throws<ArgumentNullException>(()=>
                this._ppRepo.GetPraxisCount(id, null));
        }

        [Test]
        public void TestGetPraxisCountNoPPRepo()
        {
            var id = new Id(2);
            var set = new HashSet<Name>();
            set.Add(new Name("x"));
            this._ppRepo.Data.Remove(typeof(UnsafePraxisParticipant).FullName);
            Assert.AreEqual(0, this._ppRepo.GetPraxisCount(id, set));
        }

        [Test]
        public void TestGetPraxisCountNoMatches()
        {
            Assert.AreEqual(0, this._ppRepo
                .GetPraxisCount(this._charId0, new HashSet<Name>()));

            Assert.AreEqual(0, this._ppRepo
                .GetPraxisCount(new Id(5), this._goodStatuses));

            var badStatuses = new HashSet<Name>();
            badStatuses.Add(this._badStatus);
            Assert.AreEqual(0, this._ppRepo
                .GetPraxisCount(this._charId0, badStatuses));
        }

        [Test]
        public void TestGetPraxisCountHappy()
        {
            Assert.AreEqual(2, this._ppRepo
                .GetPraxisCount(this._charId0, this._goodStatuses));
        }

        [Test]
        public void TestGetByMetaTaskId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._praxisRepo.GetByMetaTaskId(null));
            Assert.Throws<ArgumentException>(()=>
                this._praxisRepo.GetByMetaTaskId(new Id(32423)));

            var mtRepo = new RAMMetaTaskRepo();
            var mt0 = new UnsafeMetaTask(
                new Name("x"),
                new Name("y"),
                "q",
                new PointTotal(20),
                true
            );
            var mt1 = new UnsafeMetaTask(
                new Name("q"),
                new Name("0"),
                "x",
                new PointTotal(20),
                true
            );
            mtRepo.Insert(mt0);
            mtRepo.Insert(mt1);
            mtRepo.Save();
            this._praxisRepo.Insert(this._p2);
            this._praxisRepo.Save();

            this._p0.MetaTaskId = mt0.Id;
            this._praxisRepo.Update(this._p0);
            this._praxisRepo.Save();
            var praxises = this._praxisRepo
                .GetByMetaTaskId(mt0.Id).ToList<IPraxis>();
            Assert.AreEqual(1, praxises.Count());
            foreach (UnsafePraxis p in praxises)
                Assert.AreEqual(this._p0.Id, p.Id);

            this._p1.MetaTaskId = mt0.Id;
            this._praxisRepo.Update(this._p1);
            this._praxisRepo.Save();
            praxises = this._praxisRepo
                .GetByMetaTaskId(mt0.Id).ToList<IPraxis>();
            Assert.AreEqual(2, praxises.Count());
            Assert.AreEqual(this._p0.Id, praxises[0].Id);
            Assert.AreEqual(this._p1.Id, praxises[1].Id);

            this._p2.MetaTaskId = mt1.Id;
            this._praxisRepo.Update(this._p2);
            this._praxisRepo.Save();
            praxises = this._praxisRepo
                .GetByMetaTaskId(mt1.Id).ToList<IPraxis>();
            Assert.AreEqual(1, praxises.Count());
            foreach (UnsafePraxis p in praxises)
                Assert.AreEqual(this._p2.Id, p.Id);
        }

        [Test]
        public void TestGetByTaskIdSad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._praxisRepo.GetByTaskId(null));
            Assert.Throws<ArgumentException>(()=>
                this._praxisRepo.GetByTaskId(new Id(32423)));
        }

        [Test]
        public void TestGetByTaskId_taskId0()
        {
            this._praxisRepo.Insert(this._p2);
            this._praxisRepo.Save();

            var tasks = this._praxisRepo.GetByTaskId(this._taskId0).ToList();
            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual(this._p0.Id, tasks[0].Id);
            Assert.AreEqual(this._p1.Id, tasks[1].Id);
        }

        [Test]
        public void TestGetByTaskId_taskId1()
        {
            this._praxisRepo.Insert(this._p2);
            this._praxisRepo.Save();

            var tasks = this._praxisRepo.GetByTaskId(this._taskId1).ToList();
            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual(this._p2.Id, tasks[0].Id);
        }

        public class TestPraxisParticipantRepoExposedData
            : RAMPraxisParticipantRepo
        {
            public Dictionary<string, EntityData> Data { get { return _data; } }
        }
    }
}