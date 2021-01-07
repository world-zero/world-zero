using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public interface IEntitySelfRelation<TId, TBuiltIn>
        : IEntityRelation<TId, TBuiltIn, TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    { }
}