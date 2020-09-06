using System;
using System.Collections.Generic;

namespace WorldZero.Common.Interface.DTO
{
    /// <summary>
    /// A DTO (Data Transfer Object) with two ISingleValueObjects mapped as
    /// the left and right IDs.
    /// </summary>
    public abstract class IDualDTO
        <TLeftSingleValObj, TLeftBuiltIn, TRightSingleValObj, TRightBuiltIn>
        : IValueObject
        where TLeftSingleValObj  : ISingleValueObject<TLeftBuiltIn>
        where TRightSingleValObj : ISingleValueObject<TRightBuiltIn>
    {
        public abstract IDualDTO
        <TLeftSingleValObj, TLeftBuiltIn, TRightSingleValObj, TRightBuiltIn>
        DeepCopy();

        public IDualDTO(TLeftSingleValObj leftId, TRightSingleValObj rightId)
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public TLeftSingleValObj LeftId
        {
            get { return this._leftId; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("LeftId");
                this._leftId = value;
            }
        }
        protected TLeftSingleValObj _leftId;

        public TRightSingleValObj RightId
        {
            get { return this._rightId; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("RightId");
                this._rightId = value;
            }
        }
        protected TRightSingleValObj _rightId;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.LeftId;
            yield return this.RightId;
        }
    }
}