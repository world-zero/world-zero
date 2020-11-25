using System;
using System.Collections.Generic;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.ValueObject.DTO.Entity.Relation
{
    /// <summary>
    /// A DTO (Data Transfer Object) with two ISingleValueObjects mapped as
    /// the left and right IDs.
    /// </summary>
    public class RelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IValueObject
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        public virtual RelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        Clone()
        {
            return new RelationDTO
                <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.LeftId,
                this.RightId
            );
        }

        public RelationDTO(TLeftId leftId, TRightId rightId)
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

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.LeftId;
            yield return this.RightId;
        }
    }
}