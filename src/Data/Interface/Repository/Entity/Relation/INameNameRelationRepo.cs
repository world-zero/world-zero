using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface INameNameRelationRepo<TNameNameRelation>
        : IEntityRelationRepo<TNameNameRelation, Name, string, Name, string>
        where TNameNameRelation : INameNameRelation
    { }
}