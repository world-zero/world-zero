using System;
using System.Collections.Generic;
using System.Linq;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Relation
{
    [TestFixture]
    public class TestRAMUnsafeVoteRepo
    {
        private RAMUnsafeVoteRepo _repo;
        private Id _ppId0;
        private Id _ppId1;
        private Id _cId0;
        private Id _cId1;
        private UnsafeVote _v0;
        private UnsafeVote _v1;
        private UnsafeVote _vAlt;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMUnsafeVoteRepo();
            this._ppId0 = new Id(1);
            this._ppId1 = new Id(2);
            this._cId0 = new Id(3);
            this._cId1 = new Id(4);
            this._v0 = new UnsafeVote(this._cId0, this._ppId0, new PointTotal(3));
            this._v1 = new UnsafeVote(this._cId1, this._ppId0, new PointTotal(3));
            this._vAlt = new UnsafeVote(this._cId0, this._ppId1, new PointTotal(3));
            this._repo.Insert(this._v0);
            this._repo.Insert(this._v1);
            this._repo.Insert(this._vAlt);
            this._repo.Save();
        }

        [TearDown]
        public void Teardown()
        {
            this._repo.CleanAll();
        }

        [Test]
        public void TestGetIdsByPraxisParticipantId()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._repo.GetIdsByPraxisParticipantId(null).FirstOrDefault());
            Assert.Throws<ArgumentException>(()=>this._repo
                .GetIdsByPraxisParticipantId(new Id(90000000)).FirstOrDefault());

            var expected = new HashSet<Id>();
            expected.Add(this._v0.Id);
            expected.Add(this._v1.Id);
            var results = this._repo.GetIdsByPraxisParticipantId(this._ppId0).ToHashSet();
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetCharacterIdsByPraxisParticipantIds()
        {
            Assert.Throws<ArgumentNullException>(()=>this._repo.
                GetCharacterIdsByPraxisParticipantIds(null).FirstOrDefault());
            var l = new List<Id>();
            Assert.Throws<ArgumentException>(()=>this._repo.
                GetCharacterIdsByPraxisParticipantIds(l).FirstOrDefault());

            l.Add(this._v0.PraxisParticipantId);
            l.Add(this._v1.PraxisParticipantId);
            var expected = new HashSet<Id>();
            expected.Add(this._v0.CharacterId);
            expected.Add(this._v1.CharacterId);
            var results = this._repo.GetCharacterIdsByPraxisParticipantIds(l).ToHashSet();
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetCharacterIdsByPraxisParticipantIdsOther()
        {
            Assert.Throws<ArgumentNullException>(()=>this._repo.
                GetCharacterIdsByPraxisParticipantIds(null).FirstOrDefault());
            var l = new List<Id>();
            Assert.Throws<ArgumentException>(()=>this._repo.
                GetCharacterIdsByPraxisParticipantIds(l).FirstOrDefault());

            l.Add(this._v0.PraxisParticipantId);
            l.Add(this._vAlt.PraxisParticipantId);
            var expected = new HashSet<Id>();
            expected.Add(this._v0.CharacterId);
            expected.Add(this._v1.CharacterId);
            expected.Add(this._vAlt.CharacterId);
            var results = this._repo.GetCharacterIdsByPraxisParticipantIds(l).ToHashSet();
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetIdsByCharacterId()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._repo.GetIdsByCharacterId(null).FirstOrDefault());
            Assert.Throws<ArgumentException>(()=>this._repo
                .GetIdsByCharacterId(new Id(90000000)).FirstOrDefault());

            var expected = new HashSet<Id>();
            expected.Add(this._v1.Id);
            var results = this._repo.GetIdsByCharacterId(this._cId1).ToHashSet();
            Assert.AreEqual(expected, results);
        }
    }
}