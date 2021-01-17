using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Relation
{
    [TestFixture]
    public class TestMetaTaskFlagDTO
    {
        private Id _id;
        private Id _leftId;
        private MetaTaskFlagDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(423);
            this._leftId = new Id(980);
            this._dto = new MetaTaskFlagDTO(this._id, this._leftId);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._leftId, this._dto.LeftId);
            Assert.IsNull(this._dto.RightId);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as MetaTaskFlagDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new MetaTaskFlagDTO(this._id, this._leftId);
            Assert.AreEqual(this._dto, alt);
            alt = new MetaTaskFlagDTO(null, this._leftId);
            Assert.AreNotEqual(this._dto, alt);
            alt = new MetaTaskFlagDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new MetaTaskFlagDTO(this._id, this._leftId);
            Assert.AreEqual(this._dto, alt);
            alt = new MetaTaskFlagDTO(null, this._leftId);
            Assert.AreNotEqual(this._dto, alt);
            alt = new MetaTaskFlagDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<MetaTaskFlagDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}