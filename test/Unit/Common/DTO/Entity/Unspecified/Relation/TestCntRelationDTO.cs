using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Unspecified.Primary
{
    [TestFixture]
    public class TestCntRelationDTO
    {
        private Id _id;
        private Name _leftId;
        private Name _rightId;
        private int _cnt;
        private CntRelationDTO<Name, string, Name, string> _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(5);
            this._leftId = new Name("Left");
            this._rightId = new Name("Right");
            this._cnt = 5;
            this._dto = new CntRelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId,
                this._cnt
            );
        }

        [Test]
        public void TestConstructor()
        {
            this._dto = new CntRelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId,
                this._cnt
            );
            Assert.AreEqual(this._id, this._dto.Id);
            Assert.AreEqual(this._leftId, this._dto.LeftId);
            Assert.AreEqual(this._rightId, this._dto.RightId);
            Assert.AreEqual(this._cnt, this._dto.Count);

            this._dto = new CntRelationDTO<Name, string, Name, string>();
            Assert.IsNull(this._dto.Id);
            Assert.IsNull(this._dto.LeftId);
            Assert.IsNull(this._dto.RightId);
            Assert.AreEqual(0, this._dto.Count);
        }

        [Test]
        public void TestClone()
        {
            var clone =
                this._dto.Clone() as CntRelationDTO<Name, string, Name, string>;
            Assert.AreEqual(this._dto, clone);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new CntRelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId,
                this._cnt
            );
            Assert.AreEqual(this._dto, alt);
            alt = new CntRelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId
            );
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new CntRelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId,
                this._cnt
            );
            Assert.AreEqual(this._dto, alt);
            alt = new CntRelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId
            );
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<CntRelationDTO<Name, string, Name, string>>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}