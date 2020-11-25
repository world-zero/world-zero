using System;
using System.Linq;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Repository.RAM.Entity.Relation
{
    [TestFixture]
    public class TestRAMVoteRepo
    {
        private RAMVoteRepo _repo;
        private Id _pId0;
        private Id _pId1;
        private Id _cId0;
        private Id _cId1;
        private Id _recChar0;
        private Id _recChar1;
        private Vote _v0;
        private Vote _v1;
        private Vote _vAlt;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMVoteRepo();
            this._pId0 = new Id(1);
            this._pId1 = new Id(2);
            this._cId0 = new Id(3);
            this._cId1 = new Id(4);
            this._recChar0 = new Id(10);
            this._recChar1 = new Id(20);
            this._v0 = new Vote(
                this._cId0, this._pId0, this._recChar0, new PointTotal(3));
            this._v1 = new Vote(
                this._cId1, this._pId0, this._recChar0, new PointTotal(3));
            this._vAlt = new Vote(
                this._cId0, this._pId1, this._recChar1, new PointTotal(3));
        }

        [TearDown]
        public void Teardown()
        {
            this._repo.CleanAll();
        }

        [Test]
        public void TestGetByPlayerId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.GetPraxisVoters(null));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetPraxisVoters(new Id(234)));

            this._repo.Insert(this._v0);
            this._repo.Save();
            var ids = this._repo.GetPraxisVoters(this._pId0).ToList();
            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(this._cId0, ids[0]);

            this._repo.Insert(this._v1);
            this._repo.Save();
            ids = this._repo.GetPraxisVoters(this._pId0).ToList();
            Assert.AreEqual(2, ids.Count);
            Assert.AreEqual(this._cId0, ids[0]);
            Assert.AreEqual(this._cId1, ids[1]);

            this._repo.Insert(this._vAlt);
            this._repo.Save();
            ids = this._repo.GetPraxisVoters(this._pId1).ToList();
            Assert.AreEqual(1, ids.Count);
            Assert.AreEqual(this._cId0, ids[0]);
        }

        [Test]
        public void TestDeleteByReceivingCharId()
        {
            this._repo.Insert(this._v0);
            this._repo.Insert(this._v1);
            this._repo.Insert(this._vAlt);
            this._repo.Save();

            this._repo.DeleteByReceivingCharId(this._recChar0);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._v0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._v1.Id));
            var actual = this._repo.GetAll();
            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(this._vAlt.Id, actual.First().Id);
        }
    }
}