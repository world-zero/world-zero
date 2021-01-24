using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntitySelfRelationDTO{TId, TBuiltIn}"/>
    public interface IEntitySelfRelation<TId, TBuiltIn>
        : IEntityRelation<TId, TBuiltIn, TId, TBuiltIn>,
        IEntitySelfRelationDTO<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    { }
}