using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelateRelationalDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TLEntityRelation, TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn, TLRelationDTO, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public abstract class ABCEntityRelateRelationalDel
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
    : ABCEntityRelationDel
    <
        TEntityRelation,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRightEntity,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >,
    IEntityRelateRelationalDel
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
        where TEntityRelation : class, IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>

        where TLEntityRelation : IEntityRelation
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>
        where TLLeftId  : ISingleValueObject<TLLeftBuiltIn>
        where TLRightId : ISingleValueObject<TLRightBuiltIn>
        where TLRelationDTO : RelationDTO
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>

        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        public ABCEntityRelateRelationalDel(
            IEntityRelationRepo
            <
                TEntityRelation,
                TLeftId,
                TLeftBuiltIn,
                TRightId,
                TRightBuiltIn,
                TRelationDTO
            >
            repo
        )
            : base(repo)
        { }
    }
}