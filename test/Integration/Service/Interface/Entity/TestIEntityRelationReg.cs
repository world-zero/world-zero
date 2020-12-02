using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Primary;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Interface.Entity.Registration;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntityRelationReg
    {
        private IVoteRepo _voteRepo;
        private ICharacterRepo _characterRepo;
        private IPraxisParticipantRepo _ppRepo;
        private TestEntityRelationReg _reg;

        [SetUp]
        public void Setup()
        {
            this._voteRepo = new RAMVoteRepo();
            this._characterRepo = new RAMCharacterRepo();
            this._ppRepo = new RAMPraxisParticipantRepo();
            this._reg = new TestEntityRelationReg(
                this._voteRepo,
                this._characterRepo,
                this._ppRepo
            );
        }

        [TearDown]
        public void TearDown()
        {
            if (this._voteRepo.IsTransactionActive())
            {
                this._voteRepo.DiscardTransaction();
                throw new InvalidOperationException("A test exits with an active transaction.");
            }
            this._voteRepo.CleanAll();
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityRelationReg(
                    null,
                    this._characterRepo,
                    this._ppRepo));
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityRelationReg(
                    this._voteRepo,
                    null,
                    this._ppRepo));
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityRelationReg(
                    this._voteRepo,
                    this._characterRepo,
                    null));
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._reg.Register(null));

            Vote v = new Vote(new Id(0), new Id(0), new PointTotal(1));
            Assert.Throws<ArgumentException>(()=>
                this._reg.Register(v));

            var c = new Character(new Name("foo"), new Id(2));
            this._characterRepo.Insert(c);
            this._characterRepo.Save();
            v.CharacterId = c.Id;
            Assert.Throws<ArgumentException>(()=>
                this._reg.Register(v));

            var pp =
                new PraxisParticipant(new Id(20), c.Id);
            this._ppRepo.Insert(pp);
            this._ppRepo.Save();
            Assert.Throws<ArgumentException>(()=>this._reg.Register(v));

            v.PraxisParticipantId = pp.Id;
            this._reg.Register(v);
            Assert.IsTrue(v.IsIdSet());
        }
    }

    public class TestEntityRelationReg
        : IEntityRelationReg
        <
            Vote,
            Character,
            Id,
            int,
            PraxisParticipant,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        public TestEntityRelationReg(
            IVoteRepo repo,
            ICharacterRepo characterRepo,
            IPraxisParticipantRepo praxisRepo
        )
            : base(repo, characterRepo, praxisRepo)
        { }
    }
}