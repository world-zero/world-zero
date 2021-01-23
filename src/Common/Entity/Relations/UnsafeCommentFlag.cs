using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="ICommentFlag"/>
    public class UnsafeCommentFlag
        : ABCFlaggedEntity<Id, int>, ICommentFlag
    {
        public Id CommentId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public UnsafeCommentFlag(Id commentId, Name flagId)
            : base(commentId, flagId)
        { }

        public UnsafeCommentFlag(Id id, Id commentId, Name flagId)
            : base(id, commentId, flagId)
        { }

        public UnsafeCommentFlag(NoIdRelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeCommentFlag(Id id, NoIdRelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        public UnsafeCommentFlag(ICommentFlagDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        { }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafeCommentFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}