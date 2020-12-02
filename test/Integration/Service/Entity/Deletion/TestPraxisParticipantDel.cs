using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestPraxisParticipatDel
    {
        private RAMVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMPraxisParticipantRepo _repo;
        private PraxisParticipantDel _del;

        [SetUp]
        public void Setup()
        {
            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._repo = new RAMPraxisParticipantRepo();
            this._del = new PraxisParticipantDel(this._repo, this._voteDel);
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
        public void TestConstructor()
        {
            Assert.Throws<ArgumentNullException>(()=>
                new PraxisParticipantDel(null, this._voteDel));
            Assert.Throws<ArgumentNullException>(()=>
                new PraxisParticipantDel(this._repo, null));
        }

        [Test]
        public void TestDelete()
        {
            Id id = null;
            PraxisParticipant ppX = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.Delete(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.Delete(ppX));

            // Bad: deleting the only participant.
            ppX = new PraxisParticipant(new Id(10), new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>this._del.Delete(ppX));

            // Happy: someone has joined ppX, so now ppX can leave it.
            var ppY = new PraxisParticipant(new Id(10), new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();
            this._del.Delete(ppX.Id);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
        }

        [Test]
        public void TestDeleteWithVotes()
        {
            var ppX = new PraxisParticipant(new Id(10), new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();
            var ppY = new PraxisParticipant(new Id(10), new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();

            var voteX0 = new Vote(new Id(1000), ppX.Id, new PointTotal(2));
            var voteX1 = new Vote(new Id(3200), ppX.Id, new PointTotal(2));
            var voteY0 = new Vote(new Id(1003), ppY.Id, new PointTotal(2));
            this._voteRepo.Insert(voteX0);
            this._voteRepo.Insert(voteX1);
            this._voteRepo.Insert(voteY0);
            this._voteRepo.Save();

            this._del.Delete(ppX.Id);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.Throws<ArgumentException>(()=>
                this._voteRepo.GetById(voteX0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._voteRepo.GetById(voteX1.Id));
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
            Assert.AreEqual(voteY0.Id, this._voteRepo.GetById(voteY0.Id).Id);
        }

        [Test]
        public void TestDeleteByLeft()
        {
            Id id = null;
            Praxis p = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByLeft(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByLeft(p));

            Assert.Throws<NotSupportedException>(()=>
                this._del.DeleteByLeft(new Id(100)));
        }

        [Test]
        public void TestDeleteByPraxis()
        {
            Id id = null;
            Praxis p = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByPraxis(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByPraxis(p));

            Assert.Throws<NotSupportedException>(()=>
                this._del.DeleteByLeft(new Id(100)));
        }

        [Test]
        public void TestDeleteByRight()
        {
            Id id = null;
            Character c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByRight(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByRight(c));

            // Bad: deleting the only participant.
            var ppX = new PraxisParticipant(new Id(10), new Id(10), 2);
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByRight(ppX.RightId));

            // Happy: someone has joined ppX, so now ppX can leave it.
            var ppY = new PraxisParticipant(new Id(10), new Id(11), 1);
            this._repo.Insert(ppY);
            this._repo.Save();
            this._del.DeleteByRight(ppX.RightId);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
        }

        [Test]
        public void TestDeleteByCharacter()
        {
            Id id = null;
            Character c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByCharacter(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByCharacter(c));

            var charId = new Id(2);
            var pId0 = new Id(10);
            var pId1 = new Id(20);

            // Bad: deleting the only participant.
            var ppX = new PraxisParticipant(pId0, charId, 2);
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByCharacter(charId));

            // And now that Character is on a second praxis.
            var ppXX = new PraxisParticipant(pId1, charId);
            this._repo.Insert(ppXX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByCharacter(charId));

            // Bad: someone has joined charId, but charId can't leave it
            // since they are on another praxis by themselves.
            var ppY = new PraxisParticipant(pId0, new Id(11), 1);
            this._repo.Insert(ppY);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByCharacter(charId));

            // Happy: add someone to pId1, so now charId can leave.
            var ppZ = new PraxisParticipant(pId1, new Id(13));
            this._repo.Insert(ppZ);
            this._repo.Save();
            this._del.DeleteByCharacter(charId);
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
            Assert.AreEqual(ppZ.Id, this._repo.GetById(ppZ.Id).Id);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppXX.Id));
        }

        [Test]
        public void TestDeleteByPartialDTO()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByPartialDTO(null));

            // Bad: deleting the only participant.
            var ppX = new PraxisParticipant(new Id(10), new Id(10), 2);
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByPartialDTO(ppX.GetDTO()));

            // Happy: someone has joined ppX, so now ppX can leave it.
            var ppY = new PraxisParticipant(new Id(10), new Id(11), 1);
            this._repo.Insert(ppY);
            this._repo.Save();
            this._del.DeleteByPartialDTO(ppX.GetDTO());
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
        }

        [Test]
        public void TestDeleteByDTO()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByDTO(null));

            // Bad: deleting the only participant.
            var ppX = new PraxisParticipant(new Id(10), new Id(10), 2);
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>this._del.DeleteByDTO(
                (CntRelationDTO<Id, int, Id, int>) ppX.GetDTO()));

            // Happy: someone has joined ppX, so now ppX can leave it.
            var ppY = new PraxisParticipant(new Id(10), new Id(11), 1);
            this._repo.Insert(ppY);
            this._repo.Save();
            this._del.DeleteByDTO(
                (CntRelationDTO<Id, int, Id, int>) ppX.GetDTO());
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
        }
    }
}