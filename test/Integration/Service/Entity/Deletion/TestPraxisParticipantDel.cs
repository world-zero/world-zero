using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Deletion.Primary;
using WorldZero.Service.Entity.Deletion.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Deletion
{
    [TestFixture]
    public class TestPraxisParticipatDel
    {
        private RAMPraxisRepo _praxisRepo;
        private RAMVoteRepo _voteRepo;
        private VoteDel _voteDel;
        private RAMPraxisParticipantRepo _repo;
        private PraxisParticipantDel _del;
        private UnsafePraxis _p0;
        private UnsafePraxis _p1;

        [SetUp]
        public void Setup()
        {
            this._praxisRepo = new RAMPraxisRepo();
            this._voteRepo = new RAMVoteRepo();
            this._voteDel = new VoteDel(this._voteRepo);
            this._repo = new RAMPraxisParticipantRepo();
            this._del = new PraxisParticipantDel(
                this._repo,
                this._praxisRepo,
                this._voteDel
            );
            this._p0 = new UnsafePraxis(new Id(1), new PointTotal(1), new Name("s"));
            this._p1 = new UnsafePraxis(new Id(2), new PointTotal(1), new Name("s"));
            this._praxisRepo.Insert(this._p0);
            this._praxisRepo.Insert(this._p1);
            this._praxisRepo.Save();
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
            new PraxisParticipantDel(
                this._repo,
                this._praxisRepo,
                this._voteDel
            );
            Assert.Throws<ArgumentNullException>(()=>new PraxisParticipantDel(
                null,
                null,
                null
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisParticipantDel(
                null,
                this._praxisRepo,
                this._voteDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisParticipantDel(
                this._repo,
                null,
                this._voteDel
            ));
            Assert.Throws<ArgumentNullException>(()=>new PraxisParticipantDel(
                this._repo,
                this._praxisRepo,
                null
            ));
        }

        [Test]
        public void TestSudoDeleteByCharacter()
        {
            var commentRepo = new RAMCommentRepo();
            var commentDel = new CommentDel(commentRepo);
            var praxisTagRepo = new RAMPraxisTagRepo();
            var praxisTagDel = new PraxisTagDel(praxisTagRepo);
            var praxisFlagRepo = new RAMPraxisFlagRepo();
            var praxisFlagDel = new PraxisFlagDel(praxisFlagRepo);
            var praxisDel = new PraxisDel(
                this._praxisRepo,
                this._del,
                commentDel,
                praxisTagDel,
                praxisFlagDel
            );

            var charId0 = new Id(10);
            var charId1 = new Id(30);
            var ppX = new UnsafePraxisParticipant(this._p0.Id, charId0);
            this._repo.Insert(ppX);
            this._repo.Save();
            var ppY = new UnsafePraxisParticipant(this._p0.Id, charId1);
            this._repo.Insert(ppY);
            this._repo.Save();
            var ppZ = new UnsafePraxisParticipant(this._p1.Id, charId0);
            this._repo.Insert(ppZ);
            this._repo.Save();

            Id id = null;
            UnsafeCharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.SudoDeleteByCharacter(id, praxisDel));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.SudoDeleteByCharacter(c, praxisDel));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.SudoDeleteByCharacter(charId0, null));

            this._del.SudoDeleteByCharacter(charId0, praxisDel);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppZ.Id));
            this._repo.GetById(ppY.Id);

            this._del.SudoDeleteByCharacter(charId0, praxisDel);
            this._repo.GetById(ppY.Id);

            this._del.SudoDeleteByCharacter(charId1, praxisDel);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppY.Id));
        }

        [Test]
        public void TestSudoDeleteByCharacterDueling()
        {
            var commentRepo = new RAMCommentRepo();
            var commentDel = new CommentDel(commentRepo);
            var praxisTagRepo = new RAMPraxisTagRepo();
            var praxisTagDel = new PraxisTagDel(praxisTagRepo);
            var praxisFlagRepo = new RAMPraxisFlagRepo();
            var praxisFlagDel = new PraxisFlagDel(praxisFlagRepo);
            var praxisDel = new PraxisDel(
                this._praxisRepo,
                this._del,
                commentDel,
                praxisTagDel,
                praxisFlagDel
            );

            var p = new UnsafePraxis(
                new Id(342),
                new PointTotal(2),
                new Name("x"),
                areDueling: true
            );
            this._praxisRepo.Insert(p);
            this._praxisRepo.Save();

            var charId0 = new Id(10);
            var charId1 = new Id(30);
            var pp0 = new UnsafePraxisParticipant(p.Id, charId0);
            var pp1 = new UnsafePraxisParticipant(p.Id, charId1);
            this._repo.Insert(pp0);
            this._repo.Insert(pp1);
            this._repo.Save();

            this._del.SudoDeleteByCharacter(charId0, praxisDel);

            Assert.AreEqual(1,this._repo.GetParticipantCountViaPraxisId(p.Id));
            p = this._praxisRepo.GetById(p.Id);
            Assert.IsFalse(p.AreDueling);
        }

        [Test]
        public void TestUNSAFE_DeleteByPraxis()
        {
            Id id = null;
            UnsafePraxis p = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.UNSAFE_DeleteByPraxis(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.UNSAFE_DeleteByPraxis(p));

            var ppX = new UnsafePraxisParticipant(this._p0.Id, new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();
            var ppY = new UnsafePraxisParticipant(this._p0.Id, new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();

            var voteX0 = new UnsafeVote(new Id(1000), ppX.Id, new PointTotal(2));
            var voteX1 = new UnsafeVote(new Id(3200), ppX.Id, new PointTotal(2));
            var voteY0 = new UnsafeVote(new Id(1003), ppY.Id, new PointTotal(2));
            this._voteRepo.Insert(voteX0);
            this._voteRepo.Insert(voteX1);
            this._voteRepo.Insert(voteY0);
            this._voteRepo.Save();

            this._del.UNSAFE_DeleteByPraxis(this._p0.Id);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppY.Id));
            Assert.Throws<ArgumentException>(()=>
                this._voteRepo.GetById(voteX0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._voteRepo.GetById(voteX1.Id));
            Assert.Throws<ArgumentException>(()=>
                this._voteRepo.GetById(voteY0.Id));
        }

        [Test]
        public void TestDelete()
        {
            Id id = null;
            UnsafePraxisParticipant ppX = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.Delete(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.Delete(ppX));

            // Bad: deleting the only participant.
            ppX = new UnsafePraxisParticipant(this._p0.Id, new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>this._del.Delete(ppX));

            // Happy: someone has joined ppX, so now ppX can leave it.
            var ppY = new UnsafePraxisParticipant(this._p0.Id, new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();
            this._del.Delete(ppX.Id);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
        }

        [Test]
        public void TestDeleteWithVotes()
        {
            var ppX = new UnsafePraxisParticipant(this._p0.Id, new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();
            var ppY = new UnsafePraxisParticipant(this._p0.Id, new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();

            var voteX0 = new UnsafeVote(new Id(1000), ppX.Id, new PointTotal(2));
            var voteX1 = new UnsafeVote(new Id(3200), ppX.Id, new PointTotal(2));
            var voteY0 = new UnsafeVote(new Id(1003), ppY.Id, new PointTotal(2));
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
            UnsafePraxis p = null;
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
            UnsafePraxis p = null;
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
            UnsafeCharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByRight(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByRight(c));

            // Bad: deleting the only participant.
            var ppX = new UnsafePraxisParticipant(this._p0.Id, new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByRight(ppX.RightId));

            // Happy: someone has joined ppX, so now ppX can leave it.
            var ppY = new UnsafePraxisParticipant(this._p0.Id, new Id(11));
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
            UnsafeCharacter c = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByCharacter(id));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByCharacter(c));

            var charId = new Id(2);
            var pId0 = this._p0.Id;
            var pId1 = this._p1.Id;

            // Bad: deleting the only participant.
            var ppX = new UnsafePraxisParticipant(pId0, charId);
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByCharacter(charId));

            // And now that Character is on a second praxis.
            var ppXX = new UnsafePraxisParticipant(pId1, charId);
            this._repo.Insert(ppXX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByCharacter(charId));

            // Bad: someone has joined charId, but charId can't leave it
            // since they are on another praxis by themselves.
            var ppY = new UnsafePraxisParticipant(pId0, new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._del.DeleteByCharacter(charId));

            // Happy: add someone to pId1, so now charId can leave.
            var ppZ = new UnsafePraxisParticipant(pId1, new Id(13));
            this._repo.Insert(ppZ);
            this._repo.Save();
            this._del.DeleteByCharacter(charId);
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
            Assert.AreEqual(ppZ.Id, this._repo.GetById(ppZ.Id).Id);
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppXX.Id));
        }

        [Test]
        public void TestDeleteByDTO()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByDTO(null));

            // Bad: deleting the only participant.
            var ppX = new UnsafePraxisParticipant(this._p0.Id, new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();
            Assert.Throws<ArgumentException>(()=>this._del.DeleteByDTO(
                (RelationDTO<Id, int, Id, int>) ppX.GetDTO()));

            // Happy: someone has joined ppX, so now ppX can leave it.
            var ppY = new UnsafePraxisParticipant(this._p0.Id, new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();
            this._del.DeleteByDTO(
                (RelationDTO<Id, int, Id, int>) ppX.GetDTO());
            Assert.Throws<ArgumentException>(()=>this._repo.GetById(ppX.Id));
            Assert.AreEqual(ppY.Id, this._repo.GetById(ppY.Id).Id);
        }

        [Test]
        public void TestDuelCase()
        {
            // If a participant of a duel leaves, then the praxis is changed
            // to no longer be a duel, and the remaining participant is awarded
            // the points based off their received votes.
            var p = new UnsafePraxis(
                new Id(1),
                new PointTotal(1),
                new Name("s"),
                areDueling: true
            );
            this._praxisRepo.Insert(p);
            this._praxisRepo.Save();
            Assert.IsTrue(p.AreDueling);

            var ppX = new UnsafePraxisParticipant(p.Id, new Id(10));
            this._repo.Insert(ppX);
            this._repo.Save();

            var ppY = new UnsafePraxisParticipant(p.Id, new Id(11));
            this._repo.Insert(ppY);
            this._repo.Save();

            this._del.Delete(ppX);
            var refreshedP = this._praxisRepo.GetById(p.Id);
            Assert.IsFalse(refreshedP.AreDueling);
        }
    }
}