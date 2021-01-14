using System;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Unspecified.Relation
{
    [TestFixture]
    public class TestNoIdRelationDTO
    {
        private Id _id0;
        private Id _id1;
        private NoIdRelationDTO<Id, int, Id, int> _dto;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._dto = new NoIdRelationDTO<Id, int, Id, int>(
                this._id0, this._id1);
        }

        [Test]
        public void TestConstructor()
        {
            new NoIdRelationDTO<Id, int, Id, int>(this._id0, this._id1);
            Assert.Throws<ArgumentNullException>(
                ()=>new NoIdRelationDTO<Id, int, Id, int>(null, this._id1));
            Assert.Throws<ArgumentNullException>(
                ()=>new NoIdRelationDTO<Id, int, Id, int>(this._id1, null));
            Assert.Throws<ArgumentNullException>(
                ()=>new NoIdRelationDTO<Id, int, Id, int>(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new NoIdRelationDTO<Id, int, Id, int>(
                this._id0, this._id1);
            Assert.AreEqual(this._dto, dtoAlt);
            Assert.AreEqual(dtoAlt, this._dto);
        }

        [Test]
        public void TestEqualsAlt()
        {
            var set = new HashSet<NoIdRelationDTO<Id, int, Id, int>>();
            set.Add(this._dto);
            var clone = (NoIdRelationDTO<Id, int, Id, int>) this._dto.Clone();
            Assert.AreEqual(this._dto.GetHashCode(), clone.GetHashCode());
            Assert.IsTrue(set.Contains(this._dto));
            Assert.IsTrue(set.Contains(clone));
        }

        [Test]
        public void TestClone()
        {
            var other = (NoIdRelationDTO<Id, int, Id, int>) this._dto.Clone();
            Assert.AreEqual(this._dto.LeftId, other.LeftId);
            Assert.AreEqual(this._dto.RightId, other.RightId);
        }
    }
}