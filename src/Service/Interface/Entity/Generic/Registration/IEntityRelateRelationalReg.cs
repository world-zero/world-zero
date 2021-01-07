using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Registration
{
    /// <inheritdoc cref="IEntityRelationReg{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO"/>
    public interface IEntityRelateRelationalReg
    <
        TEntityRelation,

        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
            TLEntityRelation,
            TLLeftId,
            TLLeftBuiltIn,
            TLRightId,
            TLRightBuiltIn,
            TLRelationDTO,

        TRightEntity,
        TRightId,
        TRightBuiltIn,

        TRelationDTO
    >
    : IEntityRelationReg
    <
        TEntityRelation,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRightEntity,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        where TEntityRelation : class, IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : class, IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>

        where TLEntityRelation : class, IEntityRelation
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>
        where TLLeftId  : ABCSingleValueObject<TLLeftBuiltIn>
        where TLRightId : ABCSingleValueObject<TLRightBuiltIn>
        where TLRelationDTO : RelationDTO
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>

        where TRightEntity : class, IEntity<TRightId, TRightBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    { }
}