using System.Linq;
using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Dual;
using WorldZero.Common.Interface.DTO;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Data.Interface.Repository.Entity.RAM.Relation
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
            Assert.AreEqual(expected.SubmissionCount, actual.SubmissionCount);
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
        public void TestGetByDTO()
        {
            this._assertPraxisParticipantsEqual(
                this._pp0,
                this._repo.GetByDTO(this._pp0.GetDTO()));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetByDTO(new IdIdDTO(new Id(43), new Id(31))));
        }

        [Test]
        public void TestInsertSave()
        {
            var repo = new TestRAMEntityRelationRepo();
            Assert.AreEqual(0, repo.SavedDtos.Count);
            Assert.AreEqual(0, repo.StagedDtos.Count);

            var pp0 = new PraxisParticipant(this._id0, this._id1);
            repo.Insert(pp0);
            Assert.AreEqual(0, repo.SavedDtos.Count);
            Assert.AreEqual(1, repo.StagedDtos.Count);
            Assert.IsFalse(repo.SavedDtos.ContainsKey(pp0.GetDTO()));
            Assert.IsTrue(repo.StagedDtos.ContainsKey(pp0.GetDTO()));
            Assert.IsFalse(pp0.IsIdSet());

            repo.Save();
            Assert.AreEqual(1, repo.SavedDtos.Count);
            Assert.AreEqual(0, repo.StagedDtos.Count);
            Assert.IsTrue(repo.SavedDtos.ContainsKey(pp0.GetDTO()));
            Assert.IsFalse(repo.StagedDtos.ContainsKey(pp0.GetDTO()));
            Assert.IsTrue(pp0.IsIdSet());

            repo.Insert(new PraxisParticipant(this._id2, this._id3));
            Assert.AreEqual(1, repo.SavedDtos.Count);
            Assert.AreEqual(1, repo.StagedDtos.Count);
            Assert.Throws<ArgumentException>(()=>
                repo.Insert(new PraxisParticipant(this._id2, this._id3)));
            repo.Save();
            Assert.AreEqual(2, repo.SavedDtos.Count);
            Assert.AreEqual(0, repo.StagedDtos.Count);
        }

        [Test]
        public void TestInsertBad()
        {
            Assert.Throws<ArgumentException>(()=>this._repo.Insert(
                new PraxisParticipant(new Id(666), new Id(1), new Id(2))));
            this._repo.Insert(new PraxisParticipant(this._id0, this._id1));
            Assert.Throws<ArgumentException>(()=>
                this._repo.Insert(new PraxisParticipant(this._id0, this._id1)));
            Assert.Throws<ArgumentException>(()=>
                this._repo.Save());
        }

        [Test]
        public void TestDeleteInsertFreedDto()
        {
            this._repo.Delete(this._pp0.Id);
            Assert.AreEqual(0, this._repo.RecycledDtos.Count);
            var pp = new PraxisParticipant(this._id0, this._id1);
            this._repo.Insert(pp);
            Assert.AreEqual(1, this._repo.RecycledDtos.Count);
            this._repo.Save();
            Assert.AreEqual(0, this._repo.RecycledDtos.Count);
            this._assertPraxisParticipantsEqual(
                pp,
                this._repo.SavedDtos[pp.GetDTO()]
            );
        }

        [Test]
        public void TestUpdateNonDtoChange()
        {
            this._pp0.SubmissionCount++;
            this._repo.Update(this._pp0);
            Assert.AreEqual(3, this._repo.SavedDtos.Count);
            Assert.AreEqual(0, this._repo.StagedDtos.Count);

            this._repo.Save();
            Assert.AreEqual(3, this._repo.SavedDtos.Count);
            Assert.AreEqual(0, this._repo.StagedDtos.Count);
        }

        [Test]
        public void TestUpdateDtoPartialChange()
        {
            var id = new Id(5000);
            var oldDto = this._pp0.GetDTO();
            var newDto = new IdIdDTO(id, this._pp0.RightId);
            this._pp0.LeftId = id;
            this._repo.Update(this._pp0);
            Assert.AreEqual(3, this._repo.SavedDtos.Count);
            Assert.AreEqual(2, this._repo.StagedDtos.Count);
            Assert.IsTrue(this._repo.SavedDtos.ContainsKey(oldDto));
            Assert.IsTrue(this._repo.StagedDtos.ContainsKey(newDto));
            Assert.IsNull(this._repo.StagedDtos[oldDto]);
            Assert.AreEqual(this._pp0, this._repo.StagedDtos[newDto]);

            this._repo.Save();
            Assert.AreEqual(3, this._repo.SavedDtos.Count);
            Assert.AreEqual(0, this._repo.StagedDtos.Count);
            Assert.IsTrue(this._repo.SavedDtos.ContainsKey(newDto));
        }

        [Test]
        public void TestUpdateDtoCompleteChange()
        {
            var leftId = new Id(5000);
            var rightId = new Id(39493);
            var oldDto = this._pp0.GetDTO();
            this._pp0.LeftId = leftId;
            this._pp0.RightId = rightId;
            var newDto = new IdIdDTO(leftId, rightId);
            this._repo.Update(this._pp0);
            Assert.AreEqual(3, this._repo.SavedDtos.Count);
            Assert.AreEqual(2, this._repo.StagedDtos.Count);
            Assert.IsTrue(this._repo.SavedDtos.ContainsKey(oldDto));
            Assert.IsTrue(this._repo.StagedDtos.ContainsKey(newDto));
            Assert.IsNull(this._repo.StagedDtos[oldDto]);
            Assert.AreEqual(this._pp0, this._repo.StagedDtos[newDto]);

            this._repo.Save();
            Assert.AreEqual(3, this._repo.SavedDtos.Count);
            Assert.AreEqual(0, this._repo.StagedDtos.Count);
            Assert.IsTrue(this._repo.SavedDtos.ContainsKey(newDto));
        }

        [Test]
        public void TestUpdateSad()
        {
            var pp = new PraxisParticipant(new Id(13), new Id(137));
            this._repo.Insert(pp);
            this._repo.Save();

            var leftId = new Id(444);
            var rightId = new Id(555);
            this._pp0.LeftId = leftId;
            this._pp0.RightId = rightId;
            this._repo.Update(this._pp0);
            pp.LeftId = leftId;
            pp.RightId = rightId;
            Assert.Throws<ArgumentException>(()=>this._repo.Update(pp));
        }

        [Test]
        public void TestUpdateReallocateDto()
        {
            var oldLeft = this._pp0.LeftId;
            var oldRight = this._pp0.RightId;
            var dto = new IdIdDTO(new Id(432), new Id(890));
            this._pp0.LeftId = dto.LeftId;
            this._pp0.RightId = dto.RightId;
            this._repo.Update(this._pp0);

            var pp = new PraxisParticipant(oldLeft, oldRight);
            this._repo.Insert(pp);
            this._repo.Save();
            Assert.AreEqual(4, this._repo.SavedDtos.Count);
            Assert.AreEqual(0, this._repo.StagedDtos.Count);
            this._assertPraxisParticipantsEqual(
                pp, this._repo.GetByDTO(new IdIdDTO(oldLeft, oldRight)));
            this._assertPraxisParticipantsEqual(
                this._pp0, this._repo.GetByDTO(dto));
        }

        [Test]
        public void TestDeleteDtoTrickery()
        {
            var dto = new IdIdDTO(new Id(2321), new Id(434399));
            var pp0 = new PraxisParticipant(dto);
            this._repo.Insert(pp0);
            this._repo.Save();

            this._repo.Delete(this._pp0.Id);
            this._repo.Delete(pp0.Id);
            Assert.AreEqual(4, this._repo.SavedDtos.Count);
            Assert.AreEqual(2, this._repo.StagedDtos.Count);

            var pp1 = new PraxisParticipant(this._id0, this._id1);
            var pp2 = new PraxisParticipant(dto);
            this._repo.Insert(pp1);
            this._repo.Insert(pp2);
            Assert.AreEqual(4, this._repo.SavedDtos.Count);
            Assert.AreEqual(2, this._repo.StagedDtos.Count);

            this._repo.Save();
            Assert.AreEqual(4, this._repo.SavedDtos.Count);
            Assert.AreEqual(0, this._repo.StagedDtos.Count);
            this._assertPraxisParticipantsEqual(
                pp1, this._repo.GetByDTO(new IdIdDTO(this._id0, this._id1)));
            this._assertPraxisParticipantsEqual(
                pp2, this._repo.GetByDTO(dto));
        }

        [Test]
        public void TestDeleteHappy()
        {
            this._repo.Delete(this._pp0.Id);
            Assert.AreEqual(3, this._repo.SavedDtos.Count);
            Assert.AreEqual(1, this._repo.StagedDtos.Count);
            Assert.IsNull(this._repo.StagedDtos[this._pp0.GetDTO()]);
            Assert.AreEqual(0, this._repo.RecycledDtos.Count);

            this._repo.Save();
            Assert.AreEqual(2, this._repo.SavedDtos.Count);
            Assert.AreEqual(0, this._repo.StagedDtos.Count);
        }
    }

    public class TestRAMEntityRelationRepo
        : IRAMEntityRelationRepo<PraxisParticipant, Id, int, Id, int>
    {
        public TestRAMEntityRelationRepo()
            : base()
        { }

        public Dictionary<IDualDTO<Id,int,Id,int>, PraxisParticipant> SavedDtos
        {
            get { return this._savedDtos; }
        }

        public Dictionary<IDualDTO<Id,int,Id,int>,PraxisParticipant> StagedDtos
        {
            get { return this._stagedDtos; }
        }

        public Dictionary<IDualDTO<Id, int, Id, int>, int> RecycledDtos
        {
            get { return this._recycledDtos; }
        }
    }
}