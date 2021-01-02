using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IFlaggedEntity"/>
    /// <summary>
    /// This relation maps a Task's ID to a Flag's ID,
    /// signifying that the task has flag X.
    /// <br />
    /// Left relation: `TaskId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public interface ITaskFlag : IFlaggedEntity<Id, int>
    {
        /// <summary>
        /// TaskId is a wrapper for LeftId.
        /// </summary>
        Id TaskId { get; }
    }
}