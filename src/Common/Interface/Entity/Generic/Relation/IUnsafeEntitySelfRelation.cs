using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntitySelfRelation"/>
    public abstract class IUnsafeEntitySelfRelation<TId, TBuiltIn>
        : IUnsafeEntityRelation<TId, TBuiltIn, TId, TBuiltIn>,
          IEntitySelfRelation<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public IUnsafeEntitySelfRelation(TId leftId, TId rightId)
            : base(leftId, rightId)
        { }

        public IUnsafeEntitySelfRelation(Id id, TId leftId, TId rightId)
            : base(id, leftId, rightId)
        { }
    }
}