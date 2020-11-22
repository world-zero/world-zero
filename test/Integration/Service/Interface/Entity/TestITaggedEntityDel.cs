using System;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.RAM.Entity.Relation;
using WorldZero.Service.Interface.Entity.Deletion;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity
{
    [TestFixture]
    public class TestITaggedEntityDel
    {
        private IMetaTaskTagRepo _repo;
        private TestTaggedEntityDel _del;

        [SetUp]
        public void Setup()
        {
            this._repo = new RAMMetaTaskTagRepo();
            this._del = new TestTaggedEntityDel(this._repo);
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
                new TestEntityDelete(null));
        }

        [Test]
        public void TestDeleteSad()
        {
            Tag t = null;
            Name n = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByTag(t));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByTag(n));
        }
    }

    public class TestTaggedEntityDel : ITaggedEntityDel
    <
        MetaTaskTag,
        MetaTask,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >
    {
        public TestTaggedEntityDel(IMetaTaskTagRepo repo)
            : base(repo)
        { }
    }
}