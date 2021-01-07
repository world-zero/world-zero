using System;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Unspecified.Relation
{
    [TestFixture]
    public class TestCntRelationDTO
    {
        private Id _id0;
        private Id _id1;
        private int _cnt;
        private CntRelationDTO<Id, int, Id, int> _dto;

        [SetUp]
        public void Setup()
        {
            this._id0 = new Id(3);
            this._id1 = new Id(5);
            this._cnt = 2;
            this._dto = new CntRelationDTO<Id, int, Id, int>(
                this._id0, this._id1, this._cnt);
        }

        [Test]
        public void TestConstructor()
        {
            var dto = new CntRelationDTO<Id, int, Id, int>(
                this._id0, this._id1, this._cnt);
            Assert.AreEqual(this._id0, dto.LeftId);
            Assert.AreEqual(this._id1, dto.RightId);
            Assert.AreEqual(this._cnt, dto.Count);
            Assert.Throws<ArgumentNullException>(
                ()=>new CntRelationDTO<Id, int, Id, int>(null, this._id1, 9));
            Assert.Throws<ArgumentNullException>(
                ()=>new CntRelationDTO<Id, int, Id, int>(this._id1, null, 4));
            Assert.Throws<ArgumentNullException>(
                ()=>new CntRelationDTO<Id, int, Id, int>(null, null, 4324));
        }

        [Test]
        public void TestEquals()
        {
            var dtoAlt = new CntRelationDTO<Id, int, Id, int>(
                this._id0, this._id1, this._cnt);
            Assert.AreEqual(this._dto, dtoAlt);
            Assert.AreEqual(dtoAlt, this._dto);
        }

        [Test]
        public void TestEqualsAlt()
        {
            var set = new HashSet<CntRelationDTO<Id, int, Id, int>>();
            set.Add(this._dto);
            var clone = (CntRelationDTO<Id, int, Id, int>) this._dto.Clone();
            Assert.AreEqual(this._dto.GetHashCode(), clone.GetHashCode());
            Assert.IsTrue(set.Contains(this._dto));
            Assert.IsTrue(set.Contains(clone));
        }

        [Test]
        public void TestClone()
        {
            var other = (CntRelationDTO<Id, int, Id, int>) this._dto.Clone();
            Assert.AreEqual(this._dto.LeftId, other.LeftId);
            Assert.AreEqual(this._dto.RightId, other.RightId);
            Assert.AreEqual(this._dto.Count, other.Count);
        }
    }
}