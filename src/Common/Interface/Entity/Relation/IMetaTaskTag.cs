using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskTagDTO"/>
    public interface IMetaTaskTag : ITaggedEntity<Id, int>
    {
        Id MetaTaskId { get; }
    }
}