using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="ITaggedEntity"/>
    /// <summary>
    /// This relation maps a Meta Task's ID to a Tag's ID,
    /// signifying that the meta task has tag X.
    /// <br />
    /// Left relation: `MetaTaskId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public interface IMetaTaskTag : ITaggedEntity<Id, int>
    {
        /// <summary>
        /// MetaTaskId is a wrapper for LeftId.
        /// </summary>
        Id MetaTaskId { get; }
    }
}