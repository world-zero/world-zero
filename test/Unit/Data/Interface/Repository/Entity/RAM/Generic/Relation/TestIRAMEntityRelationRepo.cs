using System.Linq;
using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.RAM.Entity.Generic.Relation
{
    [TestFixture]
    public class TestIRAMEntityRelationRepo
    {
        private Id _id0;
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private PraxisParticipant _pp0;
        private PraxisParticipant _pp1;
        private PraxisParticipant _pp2;
        private TestRAMEntityRelationRepo _repo;

        private void _assertPraxisParticipantsEqual(
            PraxisParticipant expected,
            PraxisParticipant actual
        )
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.GetDTO(), actual.GetDTO());
            Assert.AreEqual(expected.Count, actual.Count);
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
            this._repo = new TestRAMEntityRelationRepo();
            this._repo.Insert(this._pp0);
            this._repo.Insert(this._pp1);
            this._repo.Insert(this._pp2);
            this._repo.Save();
        }

        [TearDown]
        public void TearDown()
        {
            if (this._repo.IsTransactionActive())
            {
                this._repo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._repo.CleanAll();
        }

        [Test]
        public void TestGetByLeftId()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._repo.GetByLeftId(null).FirstOrDefault());
            Assert.Throws<ArgumentException>(
                ()=>this._repo.GetByLeftId(new Id(90000000)).FirstOrDefault());

            var expected = new HashSet<PraxisParticipant>();
            expected.Add(this._pp0);
            expected.Add(this._pp2);
            var results = this._repo.GetByLeftId(this._id0).ToHashSet();
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetByRightId()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._repo.GetByRightId(null).FirstOrDefault());
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetByRightId(new Id(90000000)).FirstOrDefault());

            var expected = new HashSet<PraxisParticipant>();
            expected.Add(this._pp1);
            expected.Add(this._pp2);
            var results = this._repo.GetByRightId(this._id3).ToHashSet();
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetIdsByLeftId()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._repo.GetIdsByLeftId(null).FirstOrDefault());
            Assert.Throws<ArgumentException>(
                ()=>this._repo.GetIdsByLeftId(new Id(90000000)).FirstOrDefault());

            var expected = new HashSet<Id>();
            expected.Add(this._pp0.Id);
            expected.Add(this._pp2.Id);
            var results = this._repo.GetIdsByLeftId(this._id0).ToHashSet();
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetIdsByRightId()
        {
            Assert.Throws<ArgumentNullException>(
                ()=>this._repo.GetIdsByRightId(null).FirstOrDefault());
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetIdsByRightId(new Id(90000000)).FirstOrDefault());

            var expected = new HashSet<Id>();
            expected.Add(this._pp1.Id);
            expected.Add(this._pp2.Id);
            var results = this._repo.GetIdsByRightId(this._id3).ToHashSet();
            Assert.AreEqual(expected, results);
        }

        [Test]
        public void TestGetByDTO()
        {
            this._assertPraxisParticipantsEqual(
                this._pp0,
                this._repo.GetByDTO(
                    (CntRelationDTO<Id, int, Id, int>) this._pp0.GetDTO()));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetByDTO(new CntRelationDTO<Id, int, Id, int>(
                    new Id(43), new Id(31), 2)));
        }

        [Test]
        public void TestDeleteByLeftId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.DeleteByLeftId(null));

            this._repo.Insert(new PraxisParticipant(this._id0, this._id3));
            Assert.AreEqual(3, this._repo.SavedCount);
            Assert.AreEqual(1, this._repo.StagedCount);
            this._repo.DeleteByLeftId(this._id0);
            Assert.AreEqual(3, this._repo.StagedCount);
            this._repo.Save();
            Assert.AreEqual(1, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetByLeftId(this._id0).First());
        }

        [Test]
        public void TestDeleteByRightId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.DeleteByRightId(null));

            this._repo.Insert(new PraxisParticipant(this._id3));
            Assert.AreEqual(3, this._repo.SavedCount);
            Assert.AreEqual(1, this._repo.StagedCount);
            this._repo.DeleteByRightId(this._id3);
            Assert.AreEqual(3, this._repo.StagedCount);
            this._repo.Save();
            Assert.AreEqual(1, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetByRightId(this._id3).First());
        }

        [Test]
        public void TestDeleteByDTO()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.DeleteByDTO(null));

            var dto = this._pp0.GetDTO();
            Assert.AreEqual(3, this._repo.SavedCount);
            this._repo.DeleteByDTO(
                (CntRelationDTO<Id, int, Id, int>) this._pp0.GetDTO());
            Assert.AreEqual(1, this._repo.StagedCount);
            this._repo.Save();
            Assert.AreEqual(2, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._pp0.Id));

            var pp = new PraxisParticipant(dto.LeftId, dto.RightId);
            this._repo.Insert(pp);
            Assert.AreEqual(2, this._repo.SavedCount);
            Assert.AreEqual(1, this._repo.StagedCount);
            this._repo.DeleteByDTO(
                (CntRelationDTO<Id, int, Id, int>) pp.GetDTO());
            Assert.AreEqual(1, this._repo.StagedCount);
            this._repo.Save();
            Assert.AreEqual(2, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);
        }
    }

    public class TestRAMEntityRelationRepo
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
        public TestRAMEntityRelationRepo()
            : base()
        { }

        protected override int GetRuleCount()
        {
            var a = new PraxisParticipant(new Id(2), new Id(93));
            return a.GetUniqueRules().Count;
        }

        public int SavedCount { get { return this._saved.Count; } }
        public int StagedCount { get { return this._staged.Count; } }
    }
}