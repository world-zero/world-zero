using WorldZero.Service.Interface.Entity.Generic.Registration;
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
            UnsafeComment,
            UnsafePraxis,
            Id,
            int,
            UnsafeCharacter,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
        >
    {
        protected IUnsafeCommentRepo _commentRepo
        { get { return (IUnsafeCommentRepo) this._repo; } }

        protected IUnsafePraxisRepo _praxisRepo
        { get { return (IUnsafePraxisRepo) this._leftRepo; } }

        protected IUnsafeCharacterRepo _characterRepo
        { get { return (IUnsafeCharacterRepo) this._rightRepo; } }

        public CommentReg(
            IUnsafeCommentRepo commentRepo,
            IUnsafePraxisRepo praxisRepo,
            IUnsafeCharacterRepo characterRepo
        )
            : base(commentRepo, praxisRepo, characterRepo)
        { }
    }
}