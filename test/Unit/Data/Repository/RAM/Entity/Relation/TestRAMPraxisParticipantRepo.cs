using System;
using System.Linq;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Relation
{
    [TestFixture]
    public class TestRAMPraxisParticipantRepo
    {
        private RAMPraxisParticipantRepo _repo;
        private Id _pId0;
        private Id _pId1;
        private Id _cId0;
        private Id _cId1;
        private PraxisParticipant _pp0;
        private PraxisParticipant _pp1;
        private PraxisParticipant _ppAlt;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMPraxisParticipantRepo();
            this._pId0 = new Id(1);
            this._pId1 = new Id(2);
            this._cId0 = new Id(3);
            this._cId1 = new Id(4);
            this._pp0 = new PraxisParticipant(this._pId0, this._cId0);
            this._pp1 = new PraxisParticipant(this._pId0, this._cId1);
            this._ppAlt = new PraxisParticipant(this._pId1, this._cId0);
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
    }
}