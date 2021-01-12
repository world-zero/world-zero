using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a Task's ID to a Tag's ID,
    /// signifying that the task has tag X.
    /// <br />
    /// Left relation: `TaskId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public interface ITaskTagDTO : ITaggedDTO<Id, int>
    {
        /// <summary>
        /// TaskId is a wrapper for LeftId.
        /// </summary>
        Id TaskId { get; }
    }
}