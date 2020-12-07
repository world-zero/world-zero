using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    /// </remarks>
    public abstract class IEntitySelfRelation<TId, TBuiltIn>
        : IEntityRelation<TId, TBuiltIn, TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        public IEntitySelfRelation(TId leftId, TId rightId)
            : base(leftId, rightId)
        { }

        public IEntitySelfRelation(Id id, TId leftId, TId rightId)
            : base(id, leftId, rightId)
        { }
    }
}