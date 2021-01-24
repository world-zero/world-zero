using System;
using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestABCEntityRelation
    {
        private Id _leftId;
        private Id _rightId;
        private TestIdIdRelation _idid;

        [SetUp]
        public void Setup()
        {
            this._leftId = new Id(3);
            this._rightId = new Id(9);
            this._idid = new TestIdIdRelation(this._leftId, this._rightId);
        }

        [Test]
        public void TestConstructorArgs()
        {
            new TestIdIdRelation(new Id(1), new Id(4));
            Assert.Throws<ArgumentNullException>(
                ()=>new TestIdIdRelation(null, new Id(3)));
            Assert.Throws<ArgumentNullException>(
                ()=>new TestIdIdRelation(new Id(3), null));
            Assert.Throws<ArgumentNullException>(
                ()=>new TestIdIdRelation(null, null));
        }

        [Test]
        public void TestEquals()
        {
            var id0 = new Id(3);
            var id1 = new Id(5);
            var idid = new TestIdIdRelation(id0, id1);
            var ididCopy = new TestIdIdRelation(id0, id1);
            var ididFlipped = new TestIdIdRelation(id1, id0);
            Assert.IsFalse(idid.Equals(null));
            Assert.IsTrue(idid.Equals(idid));
            Assert.IsTrue(idid.Equals(ididCopy));
            Assert.IsTrue(ididCopy.Equals(idid));
            Assert.IsTrue(idid.Equals(ididFlipped));
            Assert.IsTrue(ididFlipped.Equals(idid));

            var name0 = new Name("DIO");
            var idname = new TestIdNameRelation(id0, name0);
            var nameid = new TestNameIdRelation(name0, id0);
            Assert.IsFalse(idid.Equals(idname));
            Assert.IsFalse(idname.Equals(idid));
            Assert.IsFalse(nameid.Equals(idname));
            Assert.IsFalse(idname.Equals(nameid));
        }

        [Test]
        public void TestGetUniqueRules()
        {
            var combos = this._idid.GetUniqueRules();
            Assert.IsNotNull(combos);
            bool hasLeftId = false;
            bool hasRightId = false;
            foreach (IEnumerable<object> iterable in combos)
            {
                hasLeftId = false;
                hasRightId = false;
                foreach (object o in iterable)
                {
                    var id = o as Id;
                    if (id != null)
                    {
                        if (id == this._leftId)
                            hasLeftId = true;
                        else if (id == this._rightId)
                            hasRightId = true;
                    }
                }

                if (hasLeftId && hasRightId)
                    break;
            }
            Assert.IsTrue(hasLeftId);
            Assert.IsTrue(hasRightId);
        }
    }

    public class TestIdIdRelation : ABCEntityRelation<Id, int, Id, int>
    {
        public TestIdIdRelation(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public TestIdIdRelation(Id id, Id leftId, Id rightId)
            : base(id, leftId, rightId)
        { }

        public override NoIdRelationDTO<Id, int, Id, int> GetNoIdRelationDTO()
        {
            return null;
        }
    }

    public class TestIdNameRelation : ABCEntityRelation<Id, int, Name, string>
    {
        public TestIdNameRelation(Id leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public TestIdNameRelation(Id id, Id leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override NoIdRelationDTO<Id, int, Name, string> GetNoIdRelationDTO()
        {
            return null;
        }
    }

    public class TestNameIdRelation : ABCEntityRelation<Name, string, Id, int>
    {
        public TestNameIdRelation(Name leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public TestNameIdRelation(Id id, Name leftId, Id rightId)
            : base(id, leftId, rightId)
        { }

        public override NoIdRelationDTO<Name, string, Id, int> GetNoIdRelationDTO()
        {
            return null;
        }
    }

    public class TestNameNameRelation : ABCEntityRelation<Name, string, Name, string>
    {
        public TestNameNameRelation(Name leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public TestNameNameRelation(Id id, Name leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override NoIdRelationDTO<Name, string, Name, string> GetNoIdRelationDTO()
        {
            return null;
        }
    }
}