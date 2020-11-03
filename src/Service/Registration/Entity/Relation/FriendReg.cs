using System;
using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    /// <remarks>
    /// This will not allow friends to be foes.
    /// </remarks>
    public class FriendReg
        : IEntityRelationReg
        <
            Friend,
            Character,
            Id,
            int,
            Character,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        protected IFriendRepo _friendRepo
        { get { return (IFriendRepo) this._repo; } }

        protected readonly IFoeRepo _foeRepo;

        public FriendReg(
            IFriendRepo friendRepo,
            ICharacterRepo characterRepo,
            IFoeRepo foeRepo
        )
            : base(friendRepo, characterRepo, characterRepo)
        {
            this._foeRepo = foeRepo;
        }

        public override Friend Register(Friend f)
        {
            this.PreRegisterChecks(f, "f");
            Friend inverseF = new Friend(f.Id, f.RightId, f.LeftId);

            Foe fMatch = null;
            Foe inverseFMatch = null;

            this._friendRepo.BeginTransaction(true);
            try
            {
                fMatch = this._foeRepo.GetByDTO(f.GetDTO());
            }
            catch (ArgumentException)
            { }
            try
            {
                inverseFMatch = this._foeRepo.GetByDTO(inverseF.GetDTO());
            }
            catch (ArgumentException)
            { }

            if (   (fMatch == null)
                && (inverseFMatch == null)   )
            {
                var r = base.Register(f);
                this._friendRepo.EndTransaction();
                return r;
            }
            else
            {
                this._friendRepo.DiscardTransaction();
                throw new ArgumentException("You cannot become friends with a foe.");
            }
        }
    }
}