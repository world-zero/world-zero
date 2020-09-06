using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation">
    public abstract class INameNameRelation
        : IEntityRelation<Name, string, Name, string>
    {
        public INameNameRelation(Name leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public INameNameRelation(Id id, Name leftId, Name rightId)
            : base(id, leftId, rightId)
        { }
    }
}