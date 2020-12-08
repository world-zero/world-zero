using System;
using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using NUnit.Framework;

namespace WorldZero.Test.Unit.Common.Interface.Entity.Generic
{
    [TestFixture]
    public class TestABCEntityRelationCnt
    {
        private Id _leftId;
        private Id _rightId;
        private int _count;
        private TestIdIdRelationCnt _idid;

        [SetUp]
        public void Setup()
        {
            this._leftId = new Id(3);
            this._rightId = new Id(9);
            this._count = 3;
            this._idid = new TestIdIdRelationCnt(
                this._leftId,
                this._rightId,
                this._count
            );
        }

        [Test]
        public void TestConstructorArgs()
        {
            new TestIdIdRelationCnt(new Id(1), new Id(4));
            new TestIdIdRelationCnt(new Id(1), new Id(4), 1);
            Assert.Throws<ArgumentException>(
                ()=>new TestIdIdRelationCnt(new Id(1), new Id(3), 0));
        }

        [Test]
        public void TestGetUniqueRules()
        {
            var combos = this._idid.GetUniqueRules();
            Assert.IsNotNull(combos);
            bool hasLeftId = false;
            bool hasRightId = false;
            bool hasCount = false;
            foreach (IEnumerable<object> iterable in combos)
            {
                hasLeftId = false;
                hasRightId = false;
                hasCount = false;
                foreach (object o in iterable)
                {
                    Id id = o as Id;
                    if (id != null)
                    {
                        if (id == this._leftId)
                            hasLeftId = true;
                        else if (id == this._rightId)
                            hasRightId = true;
                    }
                    else
                    {
                        int? value = o as int?;
                        if (value != null)
                        {
                            int v = (int) value;
                            if (v == this._count)
                                hasCount = true;
                        }
                    }
                }

                if (hasLeftId && hasRightId && hasCount)
                    break;
            }
            Assert.IsTrue(hasLeftId);
            Assert.IsTrue(hasRightId);
            Assert.IsTrue(hasCount);
        }
    }

    public class TestIdIdRelationCnt : ABCEntityRelationCnt<Id, int, Id, int>
    {
        public TestIdIdRelationCnt(Id leftId, Id rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public TestIdIdRelationCnt(Id id, Id leftId, Id rightId, int cnt=1)
            : base(id, leftId, rightId, cnt)
        { }

        public override ABCEntity<Id, int> Clone()
        {
            return new TestIdIdRelationCnt(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override RelationDTO<Id, int, Id, int> GetDTO()
        {
            return null;
        }
    }
}