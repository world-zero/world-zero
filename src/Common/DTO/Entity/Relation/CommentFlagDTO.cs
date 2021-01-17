using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="ICommentFlagDTO"/>
    public class CommentFlagDTO : FlaggedDTO<Id, int>, ICommentFlagDTO
    {
        public Id CommentId { get; }

        public CommentFlagDTO(
            Id id=null,
            Id commentId=null,
            Name flagId=null
        )
            : base(id, commentId, flagId)
        { }

        public override object Clone()
        {
            return new CommentFlagDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as CommentFlagDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}