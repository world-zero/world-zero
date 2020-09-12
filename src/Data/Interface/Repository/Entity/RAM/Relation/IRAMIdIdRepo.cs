using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Relation
{
    public abstract class IRAMIdIdRepo<TEntityRelation>
        : IRAMEntityRelationRepo<TEntityRelation, Id, int, Id, int>
        where TEntityRelation : IEntityRelation<Id, int, Id, int>
    { }
}