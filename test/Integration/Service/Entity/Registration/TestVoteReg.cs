using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Entity.Registration.Primary;
using WorldZero.Service.Entity.Registration.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Entity.Registration
{
    [TestFixture]
    public class TestVoteReg
    {
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private Id _id4;
        private RAMUnsafeFactionRepo _factionRepo;
        private RAMTaskRepo _taskRepo;
        private RAMVoteRepo _voteRepo;
        private RAMUnsafeCharacterRepo _charRepo;
        private RAMPraxisRepo _praxisRepo;
        private RAMPraxisParticipantRepo _ppRepo;
        private RAMMetaTaskRepo _mtRepo;
        private VoteReg _voteReg;
        private PraxisParticipantReg _ppReg;
        private RAMUnsafeEraRepo _eraRepo;
        private EraReg _eraReg;

        [SetUp]
        public void Setup()
        {
            this._id1 = new Id(1);
            this._id2 = new Id(2);
            this._id3 = new Id(3);
            this._id4 = new Id(4);
            this._factionRepo = new RAMUnsafeFactionRepo();
            this._taskRepo = new RAMTaskRepo();
            this._eraRepo = new RAMUnsafeEraRepo();
            this._eraReg = new EraReg(this._eraRepo);
            this._voteRepo = new RAMVoteRepo();
            this._charRepo = new RAMUnsafeCharacterRepo();
            this._praxisRepo = new RAMPraxisRepo();
            this._mtRepo = new RAMMetaTaskRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._ppReg = new PraxisParticipantReg(
                this._ppRepo,
                this._praxisRepo,
                this._charRepo,
                this._mtRepo,
                this._taskRepo,
                this._factionRepo,
                this._eraReg
            );
            this._voteReg = new VoteReg(
                this._voteRepo,
                this._charRepo,
                this._ppRepo,
                this._praxisRepo
            );
        }

        [TearDown]
        public void TearDown()
        {
            if (this._charRepo.IsTransactionActive())
            {
                this._charRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._charRepo.CleanAll();
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._voteReg.Register(null));

            // Invalid left id.
            Vote v =
                new Vote(new Id(0), new Id(0), new PointTotal(3));
            Assert.Throws<ArgumentException>(()=>
                this._voteReg.Register(v));

            // Invalid right id.
            // This requires registering a character, which needs a player.
            var player = new Player(new Name("Jack"));
            var playerRepo = new RAMPlayerRepo();
            var playerReg = new PlayerReg(playerRepo);
            playerReg.Register(player);

            var factionRepo = new RAMUnsafeFactionRepo();
            var locationRepo = new RAMLocationRepo();
            var character = new UnsafeCharacter(
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
            v.CharacterId = character.Id;

            // Someone is voting on their own PP with the same character, so we
            // need a praxis, so we need a task and status too.
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

            var eraRepo = new RAMUnsafeEraRepo();
            var eraReg = new EraReg(eraRepo);

            var pt = new PointTotal(2);
            var praxis = new Praxis(task.Id, pt, status.Id);
            var mtRepo = new RAMMetaTaskRepo();
            var praxisReg = new PraxisReg(
                this._praxisRepo,
                taskRepo,
                mtRepo,
                statusRepo,
                this._ppReg
            );
            var pp = new PraxisParticipant(character.Id);
            praxisReg.Register(praxis, pp);

            v.PraxisParticipantId = pp.Id;
            Assert.Throws<ArgumentException>(()=>this._voteReg.Register(v));

            // Someone is voting with a different character.
            var altChar = new UnsafeCharacter(new Name("Hal"), player.Id);
            charReg.Register(altChar);
            v.CharacterId = altChar.Id;
            v = new Vote(altChar.Id, pp.Id, new PointTotal(1));
            Assert.Throws<ArgumentException>(()=>this._voteReg.Register(v));

            // A happy case!
            // Create a new player/character, and vote on the existing PP.
            var newPlayer = new Player(new Name("Hal"));
            playerReg.Register(newPlayer);
            var newChar = new UnsafeCharacter(
                new Name("Inmost"),
                newPlayer.Id,
                null,
                null,
                null,
                new PointTotal(40000)
            );
            charReg.Register(newChar);
            var newPP = new PraxisParticipant(praxis.Id, altChar.Id);
            this._ppRepo.Insert(newPP);
            this._ppRepo.Save();
            var ptOld = altChar.VotePointsLeft.Get;
            var newVote =
                new Vote(newChar.Id, newPP.Id, new PointTotal(2));
            this._voteReg.Register(newVote);
            var expectedPtNew = newVote.Points.Get + ptOld;
            var refreshAltChar = this._charRepo.GetById(altChar.Id);
            Assert.AreEqual(expectedPtNew, refreshAltChar.VotePointsLeft.Get);

            // Someone is voting on the same PP again, but with a different
            // character.
            var newNewChar = new UnsafeCharacter(
                new Name("Monster Hunter"),
                newPlayer.Id
            );
            var newNewVote = new Vote(
                newNewChar.Id,
                newPP.Id,
                new PointTotal(2)
            );
            Assert.Throws<ArgumentException>(()=>
                this._voteReg.Register(newNewVote));
        }
    }
}