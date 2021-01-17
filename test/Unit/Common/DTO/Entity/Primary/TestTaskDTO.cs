using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Primary
{
    [TestFixture]
    public class TestTaskDTO
    {
        private Id _id;
        private Name _statusId;
        private TaskDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(423);
            this._statusId = new Name("foo");
            this._dto = new TaskDTO(this._id, this._statusId);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._statusId, this._dto.StatusId);
            Assert.IsNull(this._dto.Summary);
            Assert.IsNull(this._dto.FactionId);
            Assert.IsNull(this._dto.Points);
            Assert.IsNull(this._dto.Level);
            Assert.IsNull(this._dto.MinLevel);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as TaskDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new TaskDTO(this._id, this._statusId);
            Assert.AreEqual(this._dto, alt);
            alt = new TaskDTO(null, this._statusId);
            Assert.AreNotEqual(this._dto, alt);
            alt = new TaskDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new TaskDTO(this._id, this._statusId);
            Assert.AreEqual(this._dto, alt);
            alt = new TaskDTO(null, this._statusId);
            Assert.AreNotEqual(this._dto, alt);
            alt = new TaskDTO(this._id, null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<TaskDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}