using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Repository.Entity.RAM.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using NUnit.Framework;

namespace WorldZero.Test.Integration.Service.Interface.Entity.Deletion
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
            UnsafeTag t = null;
            Name n = null;
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByTag(t));
            Assert.Throws<ArgumentNullException>(()=>
                this._del.DeleteByTag(n));
        }
    }

    public class TestTaggedEntityDel : ABCTaggedEntityDel
    <
        IMetaTaskTag,
        IMetaTask,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >
    {
        public TestTaggedEntityDel(IMetaTaskTagRepo repo)
            : base((ITaggedEntityRepo<IMetaTaskTag, Id, int, RelationDTO<Id, int, Name, string>>) repo)
        { }
    }
}