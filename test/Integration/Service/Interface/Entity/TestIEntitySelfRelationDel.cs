using System;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Interface.Entity.Deletion;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestIEntitySelfRelationDel
    {
        private Id _id0;
        private Id _id1;
        private Id _id2;
        private Id _id3;
        private Foe _f0;
        private Foe _f1;
        private Foe _f2;
        private RAMFoeRepo _repo;
        private TestEntitySelfRelationDel _del;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(1);
            this._id1 = new Id(2);
            this._id2 = new Id(3);
            this._id3 = new Id(4);

            this._f0 = new Foe(this._id0, this._id1);
            this._f1 = new Foe(this._id2, this._id0);
            this._f2 = new Foe(this._id1, this._id3);

            this._repo = new RAMFoeRepo();
            this._repo.Insert(this._f0);
            this._repo.Insert(this._f1);
            this._repo.Insert(this._f2);
            this._repo.Save();

            this._del = new TestEntitySelfRelationDel(this._repo);
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
        public void TestDeleteByLeftId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByLeftId(null));

            this._del.DeleteByLeftId(this._id0);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f0.Id));
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f1.Id));
            this._repo.GetById(this._f2.Id);
        }

        [Test]
        public void TestDeleteByRightId()
        {
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByRightId(null));

            this._del.DeleteByRightId(this._id1);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f0.Id));
            this._repo.GetById(this._f1.Id);
            Assert.Throws<ArgumentException>(()=>
                this._repo.GetById(this._f2.Id));
        }
    }

    public class TestEntitySelfRelationDel : IEntitySelfRelationDel
        <Foe, Character, Id, int, RelationDTO<Id, int, Id, int>>
    {
        public TestEntitySelfRelationDel(IFoeRepo repo)
            : base(repo)
        { }
    }
}