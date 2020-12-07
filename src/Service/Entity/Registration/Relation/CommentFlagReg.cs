using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelateRelationalReg"/>
    public class CommentFlagReg
        : IEntityRelateRelationalReg
        <
            UnsafeCommentFlag,

            UnsafeComment, Id, int,
                UnsafeComment, Id, int, Id, int,
                CntRelationDTO<Id, int, Id, int>,

            UnsafeFlag, Name, string,

            RelationDTO<Id, int, Name, string>
        >
    {
        protected IUnsafeCommentFlagRepo _commentFlagRepo
        { get { return (IUnsafeCommentFlagRepo) this._repo; } }

        protected IUnsafeCommentRepo _commentRepo
        { get { return (IUnsafeCommentRepo) this._leftRepo; } }

        protected IUnsafeFlagRepo _flagRepo
        { get { return (IUnsafeFlagRepo) this._rightRepo; } }

        public CommentFlagReg(
            IUnsafeCommentFlagRepo commentFlagRepo,
            IUnsafeCommentRepo commentRepo,
            IUnsafeFlagRepo flagRepo
        )
            : base(commentFlagRepo, commentRepo, flagRepo)
        { }
    }
}