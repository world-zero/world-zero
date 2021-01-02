using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IFlaggedEntity"/>
    /// <summary>
    /// This relation maps a MetaTask's ID to a Flag's ID,
    /// signifying that the meta task has flag X.
    /// <br />
    /// Left relation: `MetaTaskId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public interface IMetaTaskFlag : IFlaggedEntity<Id, int>
    {
        /// <summary>
        /// MetaTaskId is a wrapper for LeftId.
        /// </summary>
        Id MetaTaskId { get; }
    }
}