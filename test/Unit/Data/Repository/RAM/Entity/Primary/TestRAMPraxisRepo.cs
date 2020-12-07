using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Primary
{
    [TestFixture]
    public class TestRAMPraxisRepo
    {
        private RAMPraxisRepo _praxisRepo;
        private TestPraxisParticipantRepoExposedData _ppRepo;
        private HashSet<Name> _goodStatuses;
        private Id _taskId0;
        private Id _taskId1;
        private Name _goodStatus;
        private Name _otherGoodStatus;
        private Name _badStatus;
        private Praxis _p0;
        private Praxis _p1;
        private Praxis _p2;
        private PraxisParticipant _pp0;
        private PraxisParticipant _pp1;
        private PraxisParticipant _pp2;
        private PraxisParticipant _pp3;
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
            this._p0 = new Praxis(this._taskId0,this._points,this._goodStatus);
            this._p1 = new Praxis(this._taskId0,this._points,this._goodStatus);
            this._p2 = new Praxis(this._taskId1,this._points,this._badStatus);
            this._praxisRepo.Insert(this._p0);
            this._praxisRepo.Insert(this._p1);
            this._praxisRepo.Save();
            this._charId0 = new Id(1);
            this._charId1 = new Id(2);
            this._charIdUnused = new Id(3);
            this._pp0 = new PraxisParticipant(this._p0.Id, this._charId0);
            this._pp1 = new PraxisParticipant(this._p0.Id, this._charId1);
            this._pp2 = new PraxisParticipant(this._p1.Id, this._charId0);
            this._pp3 = new PraxisParticipant(this._p2.Id, this._charId0);
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
        public void TestGetByMetaTaskId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._praxisRepo.GetByMetaTaskId(null));
            Assert.Throws<ArgumentException>(()=>
                this._praxisRepo.GetByMetaTaskId(new Id(32423)));

            var mtRepo = new RAMUnsafeMetaTaskRepo();
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
                .GetByMetaTaskId(mt0.Id).ToList<Praxis>();
            Assert.AreEqual(1, praxises.Count());
            foreach (Praxis p in praxises)
                Assert.AreEqual(this._p0.Id, p.Id);

            this._p1.MetaTaskId = mt0.Id;
            this._praxisRepo.Update(this._p1);
            this._praxisRepo.Save();
            praxises = this._praxisRepo
                .GetByMetaTaskId(mt0.Id).ToList<Praxis>();
            Assert.AreEqual(2, praxises.Count());
            Assert.AreEqual(this._p0.Id, praxises[0].Id);
            Assert.AreEqual(this._p1.Id, praxises[1].Id);

            this._p2.MetaTaskId = mt1.Id;
            this._praxisRepo.Update(this._p2);
            this._praxisRepo.Save();
            praxises = this._praxisRepo
                .GetByMetaTaskId(mt1.Id).ToList<Praxis>();
            Assert.AreEqual(1, praxises.Count());
            foreach (Praxis p in praxises)
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
    }

    public class TestPraxisParticipantRepoExposedData
        : RAMPraxisParticipantRepo
    {
        public Dictionary<string, EntityData> Data { get { return _data; } }
    }
}