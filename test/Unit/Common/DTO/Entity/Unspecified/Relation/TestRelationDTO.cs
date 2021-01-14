using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Unspecified.Primary
{
    [TestFixture]
    public class TestRelationDTO
    {
        private Id _id;
        private Name _leftId;
        private Name _rightId;
        private RelationDTO<Name, string, Name, string> _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(5);
            this._leftId = new Name("Left");
            this._rightId = new Name("Right");
            this._dto = new RelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId
            );
        }

        [Test]
        public void TestConstructor()
        {
            this._dto = new RelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId
            );
            Assert.AreEqual(this._id, this._dto.Id);
            Assert.AreEqual(this._leftId, this._dto.LeftId);
            Assert.AreEqual(this._rightId, this._dto.RightId);

            this._dto = new RelationDTO<Name, string, Name, string>();
            Assert.IsNull(this._dto.Id);
            Assert.IsNull(this._dto.LeftId);
            Assert.IsNull(this._dto.RightId);
        }

        [Test]
        public void TestClone()
        {
            var clone =
                this._dto.Clone() as RelationDTO<Name, string, Name, string>;
            Assert.AreEqual(this._dto, clone);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new RelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId
            );
            Assert.AreEqual(this._dto, alt);
            alt = new RelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId
            );
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new RelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId,
                this._rightId
            );
            Assert.AreEqual(this._dto, alt);
            alt = new RelationDTO<Name, string, Name, string>(
                this._id,
                this._leftId
            );
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<RelationDTO<Name, string, Name, string>>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}