using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IIdIdRelationRepo<TIdIdRelation>
        : IEntityRelationRepo<TIdIdRelation, Id, int, Id, int>
        where TIdIdRelation : IIdIdRelation
    { }
}