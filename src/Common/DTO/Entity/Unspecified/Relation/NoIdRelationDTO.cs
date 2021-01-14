using System;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.Entity.Unspecified.Relation
{
    /// <summary>
    /// A DTO (Data Transfer Object) with two ISingleValueObjects mapped as
    /// the left and right IDs.
    /// </summary>
    /// <remarks>
    /// This will not allow for null data.
    /// </remarks>
    public class NoIdRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> : IDTO
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        public NoIdRelationDTO(TLeftId leftId, TRightId rightId)
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public TLeftId LeftId
        {
            get { return this._leftId; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("LeftId");
                this._leftId = value;
            }
        }
        protected TLeftId _leftId;

        public TRightId RightId
        {
            get { return this._rightId; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("RightId");
                this._rightId = value;
            }
        }
        protected TRightId _rightId;

        public virtual object Clone()
        {
            return new NoIdRelationDTO
                <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(object o)
        {
            return this.Equals(o as IDTO);
        }

        public virtual bool Equals(IDTO dto)
        {
            NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> other =
                dto as NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>;

            if (other == null) return false;
            if (this.LeftId != other.LeftId) return false;
            if (this.RightId != other.RightId) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int leftHash = this.LeftId.GetHashCode();
                int rightHash = this.RightId.GetHashCode();
                return leftHash
                       * 7
                       + (rightHash * 13);
            }
        }
    }
}