using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityRelationDTO{TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn}"/>
    public interface IEntitySelfRelationDTO<TId, TBuiltIn>
        : IEntityRelationDTO<TId, TBuiltIn, TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    { }
}