using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Relation;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.DTO.Entity.Relation
{
    [TestFixture]
    public class TestCommentDTO
    {
        private Id _id;
        private string _val;
        private CommentDTO _dto;

        [SetUp]
        public void Setup()
        {
            this._id = new Id(423);
            this._val = "foo";
            this._dto = new CommentDTO(this._id, value: this._val);
        }

        [Test]
        public void TestConstructor()
        {
            // Yeah this is a little lack-luster.
            Assert.AreEqual(this._id.Get, this._dto.Id.Get);
            Assert.AreEqual(this._val, this._dto.Value);
            Assert.IsNull(this._dto.LeftId);
            Assert.IsNull(this._dto.RightId);
        }

        [Test]
        public void TestClone()
        {
            Assert.AreEqual(this._dto, this._dto.Clone() as CommentDTO);
        }

        [Test]
        public void TestEqualsObject()
        {
            object alt = new CommentDTO(this._id, value: this._val);
            Assert.AreEqual(this._dto, alt);
            alt = new CommentDTO(null, value: this._val);
            Assert.AreNotEqual(this._dto, alt);
            alt = new CommentDTO(this._id, value: null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestEqualsIDTO()
        {
            var alt = new CommentDTO(this._id, value: this._val);
            Assert.AreEqual(this._dto, alt);
            alt = new CommentDTO(null, value: this._val);
            Assert.AreNotEqual(this._dto, alt);
            alt = new CommentDTO(this._id, value: null);
            Assert.AreNotEqual(this._dto, alt);
        }

        [Test]
        public void TestGetHashCode()
        {
            var s = new HashSet<CommentDTO>();
            s.Add(this._dto);
            Assert.IsTrue(s.Contains(this._dto));
        }
    }
}