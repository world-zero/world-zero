using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    //*
    public interface IEntityRelationRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn
        >
        : IIdEntityRepo<TEntityRelation>
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        /// <summary>
        /// Return the saved relational entities that have the supplied left
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByLeftId(TLeftId id);

        /// <summary>
        /// Return the saved relational entities that have the supplied right
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByRightId(TRightId id);

        /// <summary>
        /// Return the saved relational entity that have the supplied DTO. If
        /// there is no corresponding DTO, then an exception is thrown.
        /// </summary>
        TEntityRelation GetByDTO(IRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto);
    }
    //*/
    /*
    public interface IEntityRelationRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
        >
        : IIdEntityRepo<TEntityRelation>
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TRelationDTO : IRelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        /// <summary>
        /// Return the saved relational entities that have the supplied left
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByLeftId(TLeftId id);

        /// <summary>
        /// Return the saved relational entities that have the supplied right
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByRightId(TRightId id);

        /// <summary>
        /// Return the saved relational entity that have the supplied DTO. If
        /// there is no corresponding DTO, then an exception is thrown.
        /// </summary>
        TEntityRelation GetByDTO(TRelationDTO dto);
    }
    //*/
}