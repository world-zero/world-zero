using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.DTO.Entity;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Inferface.DTO
{
    [TestFixture]
    public class TestIRelationDTO
    {
        private Id _id0;
        private Id _id1;
        private TestRelationDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._dto = new TestRelationDTO(this._id0, this._id1);
        }

        [Test]
        public void TestConstructor()
        {
            new TestRelationDTO(this._id0, this._id1);
            Assert.Throws<ArgumentNullException>(
                ()=>new TestRelationDTO(null, this._id1));
            Assert.Throws<ArgumentNullException>(
                ()=>new TestRelationDTO(this._id1, null));
            Assert.Throws<ArgumentNullException>(
                ()=>new TestRelationDTO(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new TestRelationDTO(this._id0, this._id1);
            Assert.AreEqual(this._dto, dtoAlt);
            Assert.AreEqual(dtoAlt, this._dto);
        }
    }

    public class TestRelationDTO : IRelationDTO<Id, int, Id, int>
    {
        public TestRelationDTO(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        // This is not relevant so it is not built.
        public override IRelationDTO<Id, int, Id, int> Clone()
        {
            return null;
        }
    }
}