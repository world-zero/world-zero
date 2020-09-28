using System;
using System.Collections.Generic;

namespace WorldZero.Common.Interface.DTO.Entity
{
    /// <summary>
    /// A DTO (Data Transfer Object) with two ISingleValueObjects mapped as
    /// the left and right IDs.
    /// </summary>
    public abstract class IRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IValueObject
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        public abstract IRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        Clone();

        public IRelationDTO(TLeftId leftId, TRightId rightId)
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