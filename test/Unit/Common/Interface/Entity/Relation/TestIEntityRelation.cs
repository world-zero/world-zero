using System;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Relation
{
    [TestFixture]
    public class TestIEntityRelation
    {
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
    }

    public class TestIdIdRelation : IEntityRelation<Id, int, Id, int>
    {
        public TestIdIdRelation(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public TestIdIdRelation(Id id, Id leftId, Id rightId)
            : base(id, leftId, rightId)
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new TestIdIdRelation(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }

    public class TestIdNameRelation : IEntityRelation<Id, int, Name, string>
    {
        public TestIdNameRelation(Id leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public TestIdNameRelation(Id id, Id leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new TestIdNameRelation(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }

    public class TestNameIdRelation : IEntityRelation<Name, string, Id, int>
    {
        public TestNameIdRelation(Name leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public TestNameIdRelation(Id id, Name leftId, Id rightId)
            : base(id, leftId, rightId)
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new TestNameIdRelation(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }

    public class TestNameNameRelation : IEntityRelation<Name, string, Name, string>
    {
        public TestNameNameRelation(Name leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public TestNameNameRelation(Id id, Name leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new TestNameNameRelation( this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}