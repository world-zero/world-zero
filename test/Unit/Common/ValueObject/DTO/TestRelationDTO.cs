using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.ValueObject.DTO
{
    [TestFixture]
    public class TestRelationDTO
    {
        private Id _id0;
        private Id _id1;
        private RelationDTO<Id, int, Id, int> _dto;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._dto = new RelationDTO<Id, int, Id, int>(
                this._id0, this._id1);
        }

        [Test]
        public void TestConstructor()
        {
            new RelationDTO<Id, int, Id, int>(this._id0, this._id1);
            Assert.Throws<ArgumentNullException>(
                ()=>new RelationDTO<Id, int, Id, int>(null, this._id1));
            Assert.Throws<ArgumentNullException>(
                ()=>new RelationDTO<Id, int, Id, int>(this._id1, null));
            Assert.Throws<ArgumentNullException>(
                ()=>new RelationDTO<Id, int, Id, int>(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new RelationDTO<Id, int, Id, int>(
                this._id0, this._id1);
            Assert.AreEqual(this._dto, dtoAlt);
            Assert.AreEqual(dtoAlt, this._dto);
        }
    }
}