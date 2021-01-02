using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

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

        public UnsafeCommentFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public UnsafeCommentFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal UnsafeCommentFlag(int id, int commentId, string flagId)
            : base(new Id(id), new Id(commentId), new Name(flagId))
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