using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IEntityRelationRepo
        <
            TEntityRelation,
            TLeftSingleValObj,
            TLeftBuiltIn,
            TRightSingleValObj,
            TRightBuiltIn
        >
        : IIdEntityRepo<TEntityRelation>
        where TLeftSingleValObj : ISingleValueObject<TLeftBuiltIn>
        where TRightSingleValObj : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelation
        <
            TLeftSingleValObj,
            TLeftBuiltIn,
            TRightSingleValObj,
            TRightBuiltIn
        >
    {
        /// <summary>
        /// Return the saved relational entities that have the supplied left
        /// ID as an iterable.
        /// </summary>
        IEnumerable<TEntityRelation> GetByLeftId(TLeftSingleValObj id);

        /// <summary>
        /// Return the saved relational entities that have the supplied right
        /// ID as an iterable.
        /// </summary>
        IEnumerable<TEntityRelation> GetByRightId(TRightSingleValObj id);
    }
}