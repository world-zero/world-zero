using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="ICommentFlag"/>
    public sealed class CommentFlag
        : IUnsafeFlaggedProxy<UnsafeCommentFlag, Id, int>, ICommentFlag
    {
        public CommentFlag(UnsafeCommentFlag commentFlag)
            : base(commentFlag)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new CommentFlag(this._unsafeEntity);
        }

        public Id CommentId => this._unsafeEntity.CommentId;
    }
}