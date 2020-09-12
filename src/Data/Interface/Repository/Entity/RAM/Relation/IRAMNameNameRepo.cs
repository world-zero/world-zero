using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Relation
{
    public abstract class IRAMNameNameRepo<TEntityRelation>
        : IRAMEntityRelationRepo<TEntityRelation, Name, string, Name, string>
        where TEntityRelation : IEntityRelation<Name, string, Name, string>
    { }
}