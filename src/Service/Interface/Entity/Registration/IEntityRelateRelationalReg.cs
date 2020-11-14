using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;

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
    /// `TLeftEntity`, but I am not finding any documentation about how to
    /// apply two `where` clauses to a single type, as it is necessary to 
    /// ensure that `TLeftId` and `TRightId` match `TLeftEntity` as an
    /// `IEntity`, and that the `TL*` types match it as an `IEntityRelation`.
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
        where TEntityRelation : IEntityRelation
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
        protected new IEntityRelationRepo
        <
            TLEntityRelation,
            TLLeftId,
            TLLeftBuiltIn,
            TLRightId,
            TLRightBuiltIn,
            TLRelationDTO
        >
        _leftRepo;

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