using System;
using System.Collections.Generic;

namespace WorldZero.Common.Interface.DTO.Entity
{
    /// <inheritdoc cref="IRelationDTO" />
    /// <summary>
    /// This DTO also contains a positive Count int member.
    /// </summary>
    public abstract class ICntRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        public ICntRelationDTO(TLeftId leftId, TRightId rightId, int count)
            : base(leftId, rightId)
        {
            this.Count = count;
        }

        public int Count
        {
            get { return this._count; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Count must be positive.");
                this._count = value;
            }
        }
        private int _count;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.LeftId;
            yield return this.RightId;
            yield return this.Count;
        }
    }
}