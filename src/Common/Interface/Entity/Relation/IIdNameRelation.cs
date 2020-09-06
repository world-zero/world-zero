using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation">
    public abstract class IIdNameRelation
        : IEntityRelation<Id, int, Name, string>
    {
        public IIdNameRelation(Id leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public IIdNameRelation(Id id, Id leftId, Name rightId)
            : base(id, leftId, rightId)
        { }
    }
}