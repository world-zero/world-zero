using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Registration
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    /// <summary>
    /// This class will is a relation entity registration where the left
    /// ID maps to an entity that is also a relational entity.
    /// </summary>
    /// <remarks>
    /// The generic types that start with `TL` and are followed by full words
    /// are the types used in the left entity's repo.
    /// <br/>
    /// Yes I am aware that `TLEntityRelation` is just a duplicate of
    /// `TLeftEntity`.
    /// </remarks>
    public abstract class IEntityRelateRelationalReg
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
        where TEntityRelation : UnsafeIEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : UnsafeIEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>

        where TLEntityRelation : UnsafeIEntityRelation
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>
        where TLLeftId  : ISingleValueObject<TLLeftBuiltIn>
        where TLRightId : ISingleValueObject<TLRightBuiltIn>
        where TLRelationDTO : RelationDTO
            <TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn>

        where TRightEntity : UnsafeIEntity<TRightId, TRightBuiltIn>
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

        public IEntityRelateRelationalReg(
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