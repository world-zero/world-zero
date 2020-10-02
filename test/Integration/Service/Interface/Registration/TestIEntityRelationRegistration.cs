using System;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Interface.Registration;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Registration
{
    [TestFixture]
    public class TestIEntityRelationRegistration
    {
        private IVoteRepo _voteRepo;
        private ICharacterRepo _characterRepo;
        private IPraxisRepo _praxisRepo;
        private TestEntityRelationRegistration _registration;

        [SetUp]
        public void Setup()
        {
            this._voteRepo = new RAMVoteRepo();
            this._characterRepo = new RAMCharacterRepo();
            this._praxisRepo = new RAMPraxisRepo();
            this._registration = new TestEntityRelationRegistration(
                this._voteRepo,
                this._characterRepo,
                this._praxisRepo
            );
        }

        [Test]
        public void TestConstructorBad()
        {
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityRelationRegistration(
                    null,
                    this._characterRepo,
                    this._praxisRepo));
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityRelationRegistration(
                    this._voteRepo,
                    null,
                    this._praxisRepo));
            Assert.Throws<ArgumentNullException>(()=>
                new TestEntityRelationRegistration(
                    this._voteRepo,
                    this._characterRepo,
                    null));
        }

        [Test]
        public void TestRegister()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._registration.Register(null));

            var v = new Vote(new Id(1), new Id(1), new PointTotal(4));
            Assert.Throws<ArgumentException>(()=>
                this._registration.Register(v));

            var c = new Character(new Name("foo"), new Id(2));
            this._characterRepo.Insert(c);
            this._characterRepo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._registration.Register(v));
            this._characterRepo.Delete(c.Id);
            this._characterRepo.Save();

            var p = new Praxis(new Id(2), new Name("Valid"));
            this._praxisRepo.Insert(p);
            this._praxisRepo.Save();
            Assert.Throws<ArgumentException>(()=>
                this._registration.Register(v));

            this._characterRepo.Insert(
                new Character(new Name("foo"), new Id(2))
            );
            this._characterRepo.Save();
            v.CharacterId = new Id(2);
            this._registration.Register(v);
            Assert.IsTrue(v.IsIdSet());
        }
    }

    public class TestEntityRelationRegistration
        : IEntityRelationRegistration
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
        public TestEntityRelationRegistration(
            IVoteRepo repo,
            ICharacterRepo characterRepo,
            IPraxisRepo praxisRepo
        )
            : base(repo, characterRepo, praxisRepo)
        { }
    }
}