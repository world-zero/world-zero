using System;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.RAM.Entity.Generic.Relation
{
    [TestFixture]
    public class TestIRAMEntityRelationCntRepo
    {
        private Id _id0;
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private Comment _c0;
        private Comment _c1;
        private Comment _c2;
        private TestRAMEntityRelationCntRepo _repo;

        private void _assertCommentsEqual(
            Comment expected,
            Comment actual
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
            this._c0 = new Comment(this._id0, this._id1, "x");
            this._c1 = new Comment(this._id2, this._id3, "x");
            this._c2 = new Comment(this._id0, this._id3, "x");
            this._repo = new TestRAMEntityRelationCntRepo();
            this._repo.Insert(this._c0);
            this._repo.Insert(this._c1);
            this._repo.Insert(this._c2);
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
        public void TestCountBad()
        {
            var p = new Comment(this._id0, this._id1, "x");
            this._repo.Insert(p);
            Assert.Throws<ArgumentException>(()=>this._repo.Save());
        }

        [Test]
        public void TestSequenceOfActions()
        {
            var p1 = new Comment(new Id(1), new Id(2), "x", 1);
            this._repo.Insert(p1);
            this._repo.Save();

            var p2 = new Comment(new Id(1), new Id(2), "x", 2);
            this._repo.Insert(p2);
            this._repo.Save();

            var pBad = new Comment(new Id(1), new Id(2), "x", 2);
            this._repo.Insert(pBad);
            Assert.Throws<ArgumentException>(()=>this._repo.Save());

            this._repo.Delete(p2.Id);
            this._repo.Save();

            var p3 = new Comment(new Id(1), new Id(2), "x", 3);
            this._repo.Insert(p3);
            this._repo.Save();

            var p2again = new Comment(new Id(1), new Id(2), "x", 2);
            this._repo.Insert(p2again);
            this._repo.Save();
        }

        [Test]
        public void TestGetNextCount()
        {
            int actual = this._repo.GetNextCount(
                new RelationDTO<Id, int, Id, int>(this._id0, this._id1));
            Assert.AreEqual(2, actual);

            this._repo.Insert(
                new Comment(this._id0, this._id1, "x", actual));

            actual = this._repo.GetNextCount(
                new RelationDTO<Id, int, Id, int>(this._id0, this._id1));
            Assert.AreEqual(3, actual);

            this._repo.Delete(this._c0.Id);
            this._repo.Save();
            actual = this._repo.GetNextCount(
                new RelationDTO<Id, int, Id, int>(this._id0, this._id1));
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void TestDeleteByPartialDTO()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._repo.DeleteByPartialDTO(null));

            var dto = (CntRelationDTO<Id, int, Id, int>) this._c0.GetDTO();
            var c = new Comment(dto.LeftId, dto.RightId, "x", dto.Count+1);
            this._repo.Insert(c);
            Assert.AreEqual(3, this._repo.SavedCount);
            Assert.AreEqual(1, this._repo.StagedCount);
            this._repo.DeleteByPartialDTO((RelationDTO<Id, int, Id, int>) dto);
            Assert.AreEqual(3, this._repo.SavedCount);
            Assert.AreEqual(2, this._repo.StagedCount);
            this._repo.Save();
            Assert.AreEqual(2, this._repo.SavedCount);
            Assert.AreEqual(0, this._repo.StagedCount);

            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._c0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(c.Id));
        }

        [Test]
        public void TestDeleteByDTO()
        {
            var dto = (CntRelationDTO<Id, int, Id, int>) this._c0.GetDTO();
            var c = new Comment(dto.LeftId, dto.RightId, "x", dto.Count+1);
            Assert.AreEqual(3, this._repo.SavedCount);
            this._repo.Insert(c);
            this._repo.DeleteByDTO(
                (CntRelationDTO<Id, int, Id, int>) c.GetDTO());
            this._repo.Save();
            Assert.AreEqual(3, this._repo.SavedCount);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(c.Id));
        }
    }

    public class TestRAMEntityRelationCntRepo
        : IRAMEntityRelationCntRepo
          <
            Comment,
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
            var a = new Comment(new Id(2), new Id(93), "x");
            return a.GetUniqueRules().Count;
        }

        public int SavedCount { get { return this._saved.Count; } }
        public int StagedCount { get { return this._staged.Count; } }
    }
}