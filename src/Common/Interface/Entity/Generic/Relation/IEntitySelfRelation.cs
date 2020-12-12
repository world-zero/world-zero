using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public interface IEntitySelfRelation<TId, TBuiltIn>
        : IEntityRelation<TId, TBuiltIn, TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    { }
}