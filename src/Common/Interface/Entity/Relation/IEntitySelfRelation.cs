using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Relation
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