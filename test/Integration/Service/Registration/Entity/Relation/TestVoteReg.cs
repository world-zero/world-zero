using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Registration.Entity;
using WorldZero.Service.Registration.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Registration.Entity.Relation
{
    [TestFixture]
    public class TestVoteReg
    {
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private Id _id4;
        private RAMVoteRepo _voteRepo;
        private RAMCharacterRepo _charRepo;
        private RAMPraxisRepo _praxisRepo;
        private RAMPraxisParticipantRepo _ppRepo;
        private VoteReg _voteReg;

        [SetUp]
        public void Setup()
        {
            this._id1 = new Id(1);
            this._id2 = new Id(2);
            this._id3 = new Id(3);
            this._id4 = new Id(4);
            this._voteRepo = new RAMVoteRepo();
            this._charRepo = new RAMCharacterRepo();
            this._praxisRepo = new RAMPraxisRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._voteReg = new VoteReg(
                this._voteRepo,
                this._charRepo,
                this._praxisRepo,
                this._ppRepo
            );
        }

        [TearDown]
        public void TearDown()
        {
            this._charRepo.CleanAll();
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._voteReg.Register(null));

            // Invalid left id.
            Vote v =
                new Vote(new Id(10), new Id(20), new Id(30), new PointTotal(3));
            Assert.Throws<ArgumentException>(()=>
                this._voteReg.Register(v));

            // Invalid right id.
            // This requires registering a character, which needs a player.
            var player = new Player(new Name("Jack"));
            var playerRepo = new RAMPlayerRepo();
            var playerReg = new PlayerReg(playerRepo);
            playerReg.Register(player);

            var factionRepo = new RAMFactionRepo();
            var locationRepo = new RAMLocationRepo();
            var character = new Character(
                new Name("Stinky"),
                player.Id,
                null,
                null,
                new PointTotal(50000)
            );
            var charReg = new CharacterReg(
                this._charRepo,
                playerRepo,
                factionRepo,
                locationRepo
            );
            charReg.Register(character);

            v.VotingCharacterId = character.Id;
            Assert.Throws<ArgumentException>(()=>
                this._voteReg.Register(v));

            // Someone is voting on their own praxis with the same character.
            // This require registering a praxis, which requires tasks and
            // statuses.
            var status = new Status(new Name("Active"));
            var statusRepo = new RAMStatusRepo();
            var statusReg = new StatusReg(statusRepo);
            statusReg.Register(status);

            var task = new Task(
                new Name("Factorio"),
                status.Id,
                "sdf",
                new PointTotal(20),
                new Level(1)
            );
            var taskRepo = new RAMTaskRepo();
            var taskReg = new TaskReg(taskRepo);
            taskReg.Register(task);

            var praxis = new Praxis(task.Id, status.Id);
            var praxisReg = new PraxisReg(
                this._praxisRepo,
                taskRepo,
                statusRepo
            );
            praxisReg.Register(praxis);

            var pp = new PraxisParticipant(praxis.Id, character.Id);
            var ppReg = new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo
            );
            ppReg.Register(pp);

            v.PraxisId = praxis.Id;
            Assert.Throws<ArgumentException>(()=>this._voteReg.Register(v));

            // Someone is voting with a different character.
            var altChar = new Character(new Name("Hal"), player.Id);
            charReg.Register(altChar);
            v.VotingCharacterId = altChar.Id;
            Assert.Throws<ArgumentException>(()=>this._voteReg.Register(v));

            // Happy case!
            // Create a new player/character, and vote on the existing praxis.
            var newPlayer = new Player(new Name("Hal"));
            playerReg.Register(newPlayer);
            var newChar = new Character(
                new Name("Inmost"),
                newPlayer.Id,
                null,
                null,
                null,
                new PointTotal(40000)
            );
            charReg.Register(newChar);
            this._ppRepo.Insert(new PraxisParticipant(praxis.Id, altChar.Id));
            this._ppRepo.Save();
            var newVote =
                new Vote(newChar.Id, praxis.Id, altChar.Id, new PointTotal(2));
            this._voteReg.Register(newVote);

            // Someone is voting on the same praxis again, but with a different
            // character.
            var newNewChar = new Character(
                new Name("Monster Hunter"),
                newPlayer.Id
            );
            var newNewVote = new Vote(
                newNewChar.Id,
                praxis.Id,
                altChar.Id,
                new PointTotal(2)
            );
            Assert.Throws<ArgumentException>(()=>
                this._voteReg.Register(newNewVote));
        }

        [Test]
        public void TestRegisterBadParticipant()
        {
            var c0 = new Character(new Name("f"), new Id(0));
            this._charRepo.Insert(c0);
            this._charRepo.Save();
            var c1 = new Character(new Name("g"), new Id(2));
            this._charRepo.Insert(c1);
            this._charRepo.Save();
            var p = new Praxis(new Id(234), new Name("Active"));
            this._praxisRepo.Insert(p);
            this._praxisRepo.Save();

            // The receiving character isn't real.
            var v = new Vote(c0.Id, p.Id, new Id(1000), new PointTotal(3));
            Assert.Throws<ArgumentException>(()=>
                this._voteReg.Register(v));

            // The receiving character didn't actually participate.
            v = new Vote(c0.Id, p.Id, c1.Id, new PointTotal(3));
            Assert.Throws<ArgumentException>(()=>
                this._voteReg.Register(v));

            // Happy case, just for kicks.
            this._ppRepo.Insert(new PraxisParticipant(p.Id, c1.Id));
            this._ppRepo.Save();
            v = new Vote(c0.Id, p.Id, c1.Id, new PointTotal(3));
            this._voteReg.Register(v);
        }
    }
}