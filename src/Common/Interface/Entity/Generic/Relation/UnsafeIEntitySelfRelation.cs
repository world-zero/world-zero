using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntitySelfRelation"/>
    public abstract class UnsafeIEntitySelfRelation<TId, TBuiltIn>
        : UnsafeIEntityRelation<TId, TBuiltIn, TId, TBuiltIn>,
          IEntitySelfRelation<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public UnsafeIEntitySelfRelation(TId leftId, TId rightId)
            : base(leftId, rightId)
        { }

        public UnsafeIEntitySelfRelation(Id id, TId leftId, TId rightId)
            : base(id, leftId, rightId)
        { }
    }
}