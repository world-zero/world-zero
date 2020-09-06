using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface INameNameRelationRepo<TNameNameRelation>
        : IEntityRelationRepo<TNameNameRelation, Name, string, Name, string>
        where TNameNameRelation : INameNameRelation
    { }
}