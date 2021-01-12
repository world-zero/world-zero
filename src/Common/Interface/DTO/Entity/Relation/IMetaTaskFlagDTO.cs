using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a MetaTask's ID to a Flag's ID,
    /// signifying that the meta task has flag X.
    /// <br />
    /// Left relation: `MetaTaskId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public interface IMetaTaskFlagDTO : IFlaggedDTO<Id, int>
    {
        /// <summary>
        /// MetaTaskId is a wrapper for LeftId.
        /// </summary>
        Id MetaTaskId { get; }
    }
}