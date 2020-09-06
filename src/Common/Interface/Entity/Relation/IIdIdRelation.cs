using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation">
    public abstract class IIdIdRelation
        : IEntityRelation<Id, int, Id, int>
    {
        public IIdIdRelation(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public IIdIdRelation(Id id, Id leftId, Id rightId)
            : base(id, leftId, rightId)
        { }
    }
}