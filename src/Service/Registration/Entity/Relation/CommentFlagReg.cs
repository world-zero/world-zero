using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class CommentFlagReg
        : IEntityRelateRelationalReg
        <
            CommentFlag,

            Comment, Id, int,
                Comment, Id, int, Id, int,
                CntRelationDTO<Id, int, Id, int>,

            Flag, Name, string,

            RelationDTO<Id, int, Name, string>
        >
    {
        protected ICommentFlagRepo _commentFlagRepo
        { get { return (ICommentFlagRepo) this._repo; } }

        protected ICommentRepo _commentRepo
        { get { return (ICommentRepo) this._leftRepo; } }

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._leftRepo; } }

        public CommentFlagReg(
            ICommentFlagRepo commentFlagRepo,
            ICommentRepo commentRepo,
            IFlagRepo flagRepo
        )
            : base(commentFlagRepo, commentRepo, flagRepo)
        { }
    }
}