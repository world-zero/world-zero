using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Primary;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Primary
{
    [TestFixture]
    public class TestPraxisDTO
    {
        private Id _id;
        private Name _statusId;
        private Id _taskId;
        private PraxisDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(423);
            this._statusId = new Name("foo");
            this._taskId = new Id(43);
            this._dto = new PraxisDTO(
                this._id,
                this._statusId,
                taskId: this._taskId
            );
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._statusId, this._dto.StatusId);
            Assert.AreEqual(this._taskId, this._dto.TaskId);
            Assert.IsNull(this._dto.Points);
            Assert.IsNull(this._dto.MetaTaskId);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as PraxisDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new PraxisDTO(
                this._id,
                this._statusId,
                taskId: this._taskId
            );
            Assert.AreEqual(this._dto, alt);
            alt = new PraxisDTO(
                null,
                this._statusId,
                taskId: this._taskId
            );
            Assert.AreNotEqual(this._dto, alt);
            alt = new PraxisDTO(
                this._id,
                null,
                taskId: this._taskId
            );
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new PraxisDTO(
                this._id,
                this._statusId,
                taskId: this._taskId
            );
            Assert.AreEqual(this._dto, alt);
            alt = new PraxisDTO(
                null,
                this._statusId,
                taskId: this._taskId
            );
            Assert.AreNotEqual(this._dto, alt);
            alt = new PraxisDTO(
                this._id,
                null,
                taskId: this._taskId
            );
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<PraxisDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}