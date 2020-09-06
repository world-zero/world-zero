using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IIdNameRelationRepo<TIdNameRelation>
        : IEntityRelationRepo<TIdNameRelation, Id, int, Name, string>
        where TIdNameRelation : IIdNameRelation
    { }
}