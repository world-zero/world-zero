using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="ITaskTagDTO"/>
    public interface ITaskTag : ITaggedEntity<Id, int>
    {
        Id TaskId { get; }
    }
}