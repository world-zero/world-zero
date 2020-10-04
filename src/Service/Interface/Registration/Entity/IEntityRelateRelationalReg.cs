using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;

// TODO: also create IEntityRelateRelationalsReg

namespace WorldZero.Service.Interface.Registration.Entity
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    /// <summary>
    /// This class will is a relation entity registration where the left
    /// ID maps to an entity that is also a relational entity.
    /// </summary>
    public abstract class IEntityRelateRelationalReg
    <
        TEntityRelation,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRightEntity,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    > // TODO: extend IEntityRelationReg
        where TEntityRelation : IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        // TODO: this
    {

    }
}