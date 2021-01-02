using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="ICommentReg"/>
    public class CommentReg
        : ABCEntityRelationReg
        <
            IComment,
            IPraxis,
            Id,
            int,
            ICharacter,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
        >, ICommentReg
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