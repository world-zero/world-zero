using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="ICommentFlagReg"/>
    public class CommentFlagReg
        : ABCEntityRelateRelationalReg
        <
            ICommentFlag,

            IComment, Id, int,
                IComment, Id, int, Id, int,
                NoIdCntRelationDTO<Id, int, Id, int>,

            IFlag, Name, string,

            NoIdRelationDTO<Id, int, Name, string>
        >,
        ICommentFlagReg
    {
        protected ICommentFlagRepo _commentFlagRepo
        { get { return (ICommentFlagRepo) this._repo; } }

        protected ICommentRepo _commentRepo
        { get { return (ICommentRepo) this._leftRepo; } }

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public CommentFlagReg(
            ICommentFlagRepo commentFlagRepo,
            ICommentRepo commentRepo,
            IFlagRepo flagRepo
        )
            : base(commentFlagRepo, commentRepo, flagRepo)
        { }
    }
}