using System.Collections.Generic;
using System;
using WorldZero.Data.Interface.Repository.RAM.Entity;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity
{
    [TestFixture]
    public class TestRAMPraxisRepo
    {
        private RAMPraxisRepo _praxisRepo;
        private TestPraxisParticipantRepoExposedData _ppRepo;
        private HashSet<Name> _goodStatuses;
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
            this._p0 = new Praxis(new Id(1), this._points, this._goodStatus);
            this._p1 = new Praxis(new Id(1), this._points, this._goodStatus);
            this._p2 = new Praxis(new Id(1), this._points, this._badStatus);
            this._praxisRepo.Insert(this._p0);
            this._praxisRepo.Insert(this._p1);
            this._praxisRepo.Save();
            this._charId0 = new Id(1);
            this._charId1 = new Id(2);
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
        public void TestGetPraxisCountBadArgs()
        {
            var id = new Id(2);
            var set = new HashSet<Name>();
            set.Add(new Name("x"));
            Assert.Throws<ArgumentNullException>(()=>
                this._praxisRepo.GetPraxisCount(null, set));
            Assert.Throws<ArgumentNullException>(()=>
                this._praxisRepo.GetPraxisCount(id, null));
        }

        [Test]
        public void TestGetPraxisCountNoPPRepo()
        {
            var id = new Id(2);
            var set = new HashSet<Name>();
            set.Add(new Name("x"));
            this._ppRepo.Data.Remove(typeof(PraxisParticipant).Name);
            Assert.Throws<InvalidOperationException>(()=>
                this._praxisRepo.GetPraxisCount(id, set));
        }

        [Test]
        public void TestGetPraxisCountNoMatches()
        {
            Assert.AreEqual(0, this._praxisRepo
                .GetPraxisCount(this._charId0, new HashSet<Name>()));

            Assert.AreEqual(0, this._praxisRepo
                .GetPraxisCount(new Id(5), this._goodStatuses));

            var badStatuses = new HashSet<Name>();
            badStatuses.Add(this._badStatus);
            Assert.AreEqual(0, this._praxisRepo
                .GetPraxisCount(this._charId0, badStatuses));
        }

        [Test]
        public void TestGetPraxisCountHappy()
        {
            Assert.AreEqual(2, this._praxisRepo
                .GetPraxisCount(this._charId0, this._goodStatuses));
        }
    }

    public class TestPraxisParticipantRepoExposedData
        : RAMPraxisParticipantRepo
    {
        public Dictionary<string, EntityData> Data { get { return _data; } }
    }
}