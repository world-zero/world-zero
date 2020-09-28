using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IIdIdCntRelationRepo<TIdIdCntRelation>
        : IEntityRelationRepo<TIdIdCntRelation, Id, int, Id, int>
        where TIdIdCntRelation : IIdIdCntRelation
    { }
}