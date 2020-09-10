using System;
using System.Collections.Generic;

namespace WorldZero.Common.Interface.DTO
{
    /// <summary>
    /// A DTO (Data Transfer Object) with two ISingleValueObjects mapped as
    /// the left and right IDs.
    /// </summary>
    public abstract class IDualDTO
        <TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>
        : IValueObject
        where TLeftSVO  : ISingleValueObject<TLeftBuiltIn>
        where TRightSVO : ISingleValueObject<TRightBuiltIn>
    {
        public abstract IDualDTO
        <TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>
        DeepCopy();

        public IDualDTO(TLeftSVO leftId, TRightSVO rightId)
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public TLeftSVO LeftId
        {
            get { return this._leftId; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("LeftId");
                this._leftId = value;
            }
        }
        protected TLeftSVO _leftId;

        public TRightSVO RightId
        {
            get { return this._rightId; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("RightId");
                this._rightId = value;
            }
        }
        protected TRightSVO _rightId;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.LeftId;
            yield return this.RightId;
        }
    }
}