using System;
using System.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation
{
    /// <inheritdoc cref="RelationDTO" />
    /// <summary>
    /// This DTO also contains a positive Count int member.
    /// </summary>
    public class CntRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        public CntRelationDTO(TLeftId leftId, TRightId rightId, int count)
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

        public override RelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        Clone()
        {
            return new CntRelationDTO
                <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.LeftId;
            yield return this.RightId;
            yield return this.Count;
        }
    }
}