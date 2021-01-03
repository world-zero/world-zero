using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Registration
{
    /// <inheritdoc cref="IEntityRelateRelationalReg{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TLEntityRelation, TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn, TLRelationDTO, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public abstract class ABCEntityRelateRelationalReg
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
    : ABCEntityRelationReg
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
    IEntityRelateRelationalReg
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
        where TLeftEntity : class, IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>

        where TLEntityRelation : class, IEntityRelation
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>
        where TLLeftId  : ISingleValueObject<TLLeftBuiltIn>
        where TLRightId : ISingleValueObject<TLRightBuiltIn>
        where TLRelationDTO : RelationDTO
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>

        where TRightEntity : class, IEntity<TRightId, TRightBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        protected new IEntityRelationRepo
        <
            TLEntityRelation,
            TLLeftId,
            TLLeftBuiltIn,
            TLRightId,
            TLRightBuiltIn,
            TLRelationDTO
        >
        _leftRepo
        {
            get
            {
                return this._leftRepo;
            }
        }

        public ABCEntityRelateRelationalReg(
            IEntityRelationRepo
            <
                TEntityRelation,
                TLeftId,
                TLeftBuiltIn,
                TRightId,
                TRightBuiltIn,
                TRelationDTO
            >
            repo,
            IEntityRelationRepo
            <
                TLEntityRelation,
                TLLeftId,
                TLLeftBuiltIn,
                TLRightId,
                TLRightBuiltIn,
                TLRelationDTO
            >
            leftRepo,
            IEntityRepo
            <
                TRightEntity,
                TRightId,
                TRightBuiltIn
            >
            rightRepo
        )
            : base(
                repo,
                (IEntityRepo
                <
                    TLeftEntity,
                    TLeftId,
                    TLeftBuiltIn
                >) leftRepo,
                rightRepo
            )
        { }
    }
}