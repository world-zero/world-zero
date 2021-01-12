using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a Task's ID to a Flag's ID,
    /// signifying that the praxis has flag X.
    /// <br />
    /// Left relation: `TaskId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public interface ITaskFlagDTO : IFlaggedDTO<Id, int>
    {
        /// <summary>
        /// TaskId is a wrapper for LeftId.
        /// </summary>
        Id TaskId { get; }
    }
}