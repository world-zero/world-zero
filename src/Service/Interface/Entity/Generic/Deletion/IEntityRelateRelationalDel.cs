using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    /// <summary>
    /// This class will is a relation entity deletion where the left
    /// ID maps to an entity that is also a relational entity.
    /// </summary>
    /// <remarks>
    /// The generic types that start with `TL` and are followed by full words
    /// are the types used in the left entity's repo.
    /// <br/>
    /// Yes I am aware that `TLEntityRelation` is just a duplicate of
    /// `TLeftEntity`.
    /// </remarks>
    public interface IEntityRelateRelationalDel
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
    : IEntityRelationDel
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
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>

        where TLEntityRelation : IEntityRelation
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>
        where TLLeftId  : ABCSingleValueObject<TLLeftBuiltIn>
        where TLRightId : ABCSingleValueObject<TLRightBuiltIn>
        where TLRelationDTO : RelationDTO
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>

        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    { }
}