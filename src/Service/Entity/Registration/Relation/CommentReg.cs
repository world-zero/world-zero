using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    public class CommentReg
        : IEntityRelationReg
        <
            Comment,
            Praxis,
            Id,
            int,
            Character,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
        >
    {
        protected ICommentRepo _commentRepo
        { get { return (ICommentRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected ICharacterRepo _characterRepo
        { get { return (ICharacterRepo) this._rightRepo; } }

        public CommentReg(
            ICommentRepo commentRepo,
            IPraxisRepo praxisRepo,
            ICharacterRepo characterRepo
        )
            : base(commentRepo, praxisRepo, characterRepo)
        { }
    }
}