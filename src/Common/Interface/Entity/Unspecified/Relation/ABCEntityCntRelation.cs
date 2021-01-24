using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityCntRelation"/>
    public abstract class ABCEntityCntRelation
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : ABCEntityRelation<TLeftId,TLeftBuiltIn,TRightId,TRightBuiltIn>,
          IEntityCntRelation<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        public ABCEntityCntRelation(
            TLeftId leftId, TRightId rightId, int count=1
        )
            : base(leftId, rightId)
        {
            this.Count = count;
        }

        public ABCEntityCntRelation(
            Id id, TLeftId leftId, TRightId rightId, int count=1
        )
            : base(id, leftId, rightId)
        {
            this.Count = count;
        }

        public int Count
        {
            get { return this._count; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("A count must be positive.");
                this._count = value;
            }
        }
        private int _count;

        public override bool Equals(object obj)
        {
            var other = obj as ABCEntityCntRelation
                <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>;
            if (other == null)
                return false;

            if ( (this.LeftId == other.LeftId)
              && (this.RightId == other.RightId)
              && (this.Count == other.Count) )
            {
                return true;
            }
            else if ( (this.LeftId == other.RightId)
                    && (this.RightId == other.RightId)
                    && (this.Count == other.Count) )
            {
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * this.Count;
        }
    }
}
