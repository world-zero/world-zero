using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity.Primary;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Interface.Entity.Registration;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntityRelationReg
    {
        private IVoteRepo _voteRepo;
        private ICharacterRepo _characterRepo;
        private IPraxisRepo _praxisRepo;
        private TestEntityRelationReg _registration;

        [SetUp]
        public void Setup()
        {
            this._voteRepo = new RAMVoteRepo();
            this._characterRepo = new RAMCharacterRepo();
            this._praxisRepo = new RAMPraxisRepo();
            this._registration = new TestEntityRelationReg(
                this._voteRepo,
                this._characterRepo,
                this._praxisRepo
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
                    this._praxisRepo));
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityRelationReg(
                    this._voteRepo,
                    null,
                    this._praxisRepo));
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
                this._registration.Register(null));

            var v = new Vote(new Id(1), new Id(1), new Id(5), new PointTotal(4));
            Assert.Throws<ArgumentException>(()=>
                this._registration.Register(v));

            var c = new Character(new Name("foo"), new Id(2));
            this._characterRepo.Insert(c);
            this._characterRepo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._registration.Register(v));
            this._characterRepo.Delete(c.Id);
            this._characterRepo.Save();

            var p =
                new Praxis(new Id(2), new PointTotal(40), new Name("Valid"));
            this._praxisRepo.Insert(p);
            this._praxisRepo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._registration.Register(v));
            v.PraxisId = p.Id;

            var otherC = new Character(new Name("foo"), new Id(2));
            this._characterRepo.Insert(otherC);
            this._characterRepo.Save();
            v.VotingCharacterId = otherC.Id;
            this._registration.Register(v);
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
            Praxis,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        public TestEntityRelationReg(
            IVoteRepo repo,
            ICharacterRepo characterRepo,
            IPraxisRepo praxisRepo
        )
            : base(repo, characterRepo, praxisRepo)
        { }
    }
}