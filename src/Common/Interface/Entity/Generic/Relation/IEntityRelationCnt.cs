using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Collections;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    /// <summary>
    /// This extends IEntityRelation by adding a counter to the combo-unique
    /// rule of left ID + right ID.
    /// </summary>
    /// </remarks>
    /// Repositories of these entities are responsible for ensuring that there
    /// is a triple uniqueness on left ID, right ID, and the count, whereas
    /// the creation service classes are responsible for ensuring that the
    /// entity's Count is reasonable.
    /// </remarks>
    public abstract class IEntityRelationCnt
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        // NOTE: IEntity.Clone() is still not implemmented.

        public IEntityRelationCnt(
            TLeftId leftId, TRightId rightId, int count=1
        )
            : base(leftId, rightId)
        {
            this.Count = count;
        }

        public IEntityRelationCnt(
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
            if ( (obj == null) || (obj.GetType() != this.GetType()) )
                return false;

            IEntityRelationCnt
            <
                TLeftId,
                TLeftBuiltIn,
                TRightId,
                TRightBuiltIn
            > other;
            try
            {
                other = (IEntityRelationCnt
                <
                    TLeftId,
                    TLeftBuiltIn,
                    TRightId,
                    TRightBuiltIn
                >) obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }

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

        protected override W0Set<object> GetRelationCombo()
        {
            var s = base.GetRelationCombo();
            s.Add(this.Count);
            return s;
        }
    }
}
