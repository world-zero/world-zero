using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a comment's ID to a Flag's ID,
    /// signifying that the comment has flag X.
    /// <br />
    /// Left relation: `CommentId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public interface ICommentFlagDTO : IFlaggedDTO<Id, int>
    {
        /// <summary>
        /// CommentId wraps LeftId, which is the ID of the related Comment.
        /// </summary>
        Id CommentId { get; }
    }
}