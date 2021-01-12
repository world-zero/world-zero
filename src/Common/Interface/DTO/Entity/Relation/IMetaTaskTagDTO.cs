using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a MetaTask's ID to a Tag's ID,
    /// signifying that the praxis has tag X.
    /// <br />
    /// Left relation: `MetaTaskId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public interface IMetaTaskTagDTO : ITaggedDTO<Id, int>
    {
        /// <summary>
        /// MetaTaskId is a wrapper for LeftId.
        /// </summary>
        Id MetaTaskId { get; }
    }
}