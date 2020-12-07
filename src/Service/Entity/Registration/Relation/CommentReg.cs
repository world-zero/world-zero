using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    public class CommentReg
        : IEntityRelationReg
        <
            Comment,
            UnsafePraxis,
            Id,
            int,
            UnsafeCharacter,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
        >
    {
        protected ICommentRepo _commentRepo
        { get { return (ICommentRepo) this._repo; } }

        protected IUnsafePraxisRepo _praxisRepo
        { get { return (IUnsafePraxisRepo) this._leftRepo; } }

        protected IUnsafeCharacterRepo _characterRepo
        { get { return (IUnsafeCharacterRepo) this._rightRepo; } }

        public CommentReg(
            ICommentRepo commentRepo,
            IUnsafePraxisRepo praxisRepo,
            IUnsafeCharacterRepo characterRepo
        )
            : base(commentRepo, praxisRepo, characterRepo)
        { }
    }
}