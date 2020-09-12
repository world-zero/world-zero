using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Relation
{
    public abstract class IRAMIdNameRepo<TEntityRelation>
        : IRAMEntityRelationRepo<TEntityRelation, Id, int, Name, string>
        where TEntityRelation : IEntityRelation<Id, int, Name, string>
    { }
}