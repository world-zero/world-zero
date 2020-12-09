using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="ITaggedEntity"/>
    /// <summary>
    /// This relation maps a Task's ID to a Tag's ID,
    /// signifying that the task has tag X.
    /// <br />
    /// Left relation: `TaskId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public interface ITaskTag : ITaggedEntity<Id, int>
    {
        /// <summary>
        /// TaskId is a wrapper for LeftId.
        /// </summary>
        Id TaskId { get; }
    }
}