using System;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM.Relation
{
    [TestFixture]
    public class TestIRAMEntityRelationCntRepo
    {
        private Id _id0;
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private PraxisParticipant _pp0;
        private PraxisParticipant _pp1;
        private PraxisParticipant _pp2;
        private TestRAMEntityRelationCntRepo _repo;

        private void _assertPraxisParticipantsEqual(
            PraxisParticipant expected,
            PraxisParticipant actual
        )
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.GetDTO(), actual.GetDTO());
        }

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(5);
            this._id1 = new Id(3);
            this._id2 = new Id(15);
            this._id3 = new Id(33);
            this._pp0 = new PraxisParticipant(this._id0, this._id1);
            this._pp1 = new PraxisParticipant(this._id2, this._id3);
            this._pp2 = new PraxisParticipant(this._id0, this._id3);
            this._repo = new TestRAMEntityRelationCntRepo();
            this._repo.Insert(this._pp0);
            this._repo.Insert(this._pp1);
            this._repo.Insert(this._pp2);
            this._repo.Save();
        }

        // be sure these work correctly w/ valid/invalid cnts whose partners
        // are either all staged or all saved

        [Test]
        public void TestFinalChecks()
        {
            var p1 = new PraxisParticipant(new Id(1), new Id(2), 1);
            this._repo.Insert(p1);
            this._repo.Save();

            var p2 = new PraxisParticipant(new Id(1), new Id(2), 2);
            this._repo.Insert(p2);
            this._repo.Save();

            var pBad = new PraxisParticipant(new Id(1), new Id(2), 2);
            this._repo.Insert(pBad);
            Assert.Throws<ArgumentException>(()=>this._repo.Save());

            this._repo.Delete(p2.Id);
            this._repo.Save();

            var p3 = new PraxisParticipant(new Id(1), new Id(2), 3);
            this._repo.Insert(p3);
            this._repo.Save();

            var p2again = new PraxisParticipant(new Id(1), new Id(2), 2);
            this._repo.Insert(p2again);
            this._repo.Save();
        }
    }

    public class TestRAMEntityRelationCntRepo
        : IRAMEntityRelationRepo
          <
            PraxisParticipant,
            Id,
            int,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
          >
    {
        public TestRAMEntityRelationCntRepo()
            : base()
        { }

        protected override int GetRuleCount()
        {
            var a = new PraxisParticipant(new Id(2), new Id(93));
            return a.GetUniqueRules().Count;
        }
    }
}