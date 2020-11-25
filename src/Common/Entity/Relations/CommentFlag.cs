using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a comment's ID to a Flag's ID,
    /// signifying that the comment has flag X.
    /// <br />
    /// Left relation: `CommentId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public class CommentFlag : IIdNameRelation
    {
        /// <summary>
        /// CommentId wraps LeftId, which is the ID of the related Comment.
        /// </summary>
        public Id CommentId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// FlagId wraps RightId, which is the ID of the related Flag.
        /// </summary>
        public Name FlagId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public CommentFlag(Id commentId, Name flagId)
            : base(commentId, flagId)
        { }

        public CommentFlag(Id id, Id commentId, Name flagId)
            : base(id, commentId, flagId)
        { }

        public CommentFlag(RelationDTO<Id, int, Name, string> dto)
            : base(dto.LeftId, dto.RightId)
        { }

        public CommentFlag(Id id, RelationDTO<Id, int, Name, string> dto)
            : base(id, dto.LeftId, dto.RightId)
        { }

        internal CommentFlag(int id, int commentId, string flagId)
            : base(new Id(id), new Id(commentId), new Name(flagId))
        { }

        public override IEntity<Id, int> Clone()
        {
            return new CommentFlag(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}