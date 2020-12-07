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
            CommentFlag,

            Comment, Id, int,
                Comment, Id, int, Id, int,
                CntRelationDTO<Id, int, Id, int>,

            UnsafeFlag, Name, string,

            RelationDTO<Id, int, Name, string>
        >
    {
        protected ICommentFlagRepo _commentFlagRepo
        { get { return (ICommentFlagRepo) this._repo; } }

        protected ICommentRepo _commentRepo
        { get { return (ICommentRepo) this._leftRepo; } }

        protected IUnsafeFlagRepo _flagRepo
        { get { return (IUnsafeFlagRepo) this._rightRepo; } }

        public CommentFlagReg(
            ICommentFlagRepo commentFlagRepo,
            ICommentRepo commentRepo,
            IUnsafeFlagRepo flagRepo
        )
            : base(commentFlagRepo, commentRepo, flagRepo)
        { }
    }
}