using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityCntRelationDTO{TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn}"/>
    public interface IEntityCntRelation
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : IEntityRelation<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>,
        IEntityCntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    { }
}