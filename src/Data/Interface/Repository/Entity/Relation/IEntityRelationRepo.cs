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
        IEnumerable<TEntityRelation> GetByLeftId();
        IEnumerable<TEntityRelation> GetByRightId();
    }
}