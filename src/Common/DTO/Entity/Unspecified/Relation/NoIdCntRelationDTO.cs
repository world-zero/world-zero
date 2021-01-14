using System;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="RelationDTO" />
    /// <summary>
    /// This DTO also contains a positive Count int member.
    /// </summary>
    /// <remarks>
    /// This will not allow for null data or a negative count.
    /// </remarks>
    public class NoIdCntRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        public NoIdCntRelationDTO(TLeftId leftId, TRightId rightId, int count)
            : base(leftId, rightId)
        {
            this.Count = count;
        }

        public int Count
        {
            get { return this._count; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Count must be positive.");
                this._count = value;
            }
        }
        private int _count;

        public override object Clone()
        {
            return new NoIdCntRelationDTO
                <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }

        public override bool Equals(IDTO dto)
        {
            NoIdCntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> other =
                dto as NoIdCntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>;
            if (other == null)                 return false;
            if (this.LeftId != other.LeftId)   return false;
            if (this.RightId != other.RightId) return false;
            if (this.Count != other.Count)     return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return base.GetHashCode() * (this.Count+1);
            }
        }
    }
}