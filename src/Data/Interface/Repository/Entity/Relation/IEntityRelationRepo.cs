using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IEntityRelationRepo
        <
            TEntityRelation,
            TLeftSVO,
            TLeftBuiltIn,
            TRightSVO,
            TRightBuiltIn
        >
        : IIdEntityRepo<TEntityRelation>
        where TLeftSVO : ISingleValueObject<TLeftBuiltIn>
        where TRightSVO : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelation
        <
            TLeftSVO,
            TLeftBuiltIn,
            TRightSVO,
            TRightBuiltIn
        >
    {
        /// <summary>
        /// Return the saved relational entities that have the supplied left
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByLeftId(TLeftSVO id);

        /// <summary>
        /// Return the saved relational entities that have the supplied right
        /// ID as an iterable. If there is no corresponding ID, then an
        /// exception is thrown.
        /// </summary>
        IEnumerable<TEntityRelation> GetByRightId(TRightSVO id);

        /// <summary>
        /// Return the saved relational entity that have the supplied DTO. If
        /// there is no corresponding DTO, then an exception is thrown.
        /// </summary>
        TEntityRelation GetByDTO(
            IDualDTO<TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn> dto);
    }
}