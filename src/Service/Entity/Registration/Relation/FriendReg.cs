using System;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IFriendReg"/>
    public class FriendReg
        : ABCEntityRelationReg
        <
            IFriend,
            ICharacter,
            Id,
            int,
            ICharacter,
            Id,
            int,
            NoIdRelationDTO<Id, int, Id, int>
        >, IFriendReg
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

        public override IFriend Register(IFriend f)
        {
            IFriend inverseF = new UnsafeFriend(f.Id, f.RightId, f.LeftId);

            IFoe fMatch = null;
            IFoe inverseFMatch = null;

            this._friendRepo.BeginTransaction(true);
            try
            {
                this.GetLeftEntity(f);
                this.GetRightEntity(f);
            }
            catch (ArgumentException e)
            {
                this._friendRepo.DiscardTransaction();
                throw new ArgumentException("Could not find a related entity.", e);
            }
            try
            {
                fMatch = this._foeRepo.GetByDTO(f.GetRelationDTO());
            }
            catch (ArgumentException)
            { }
            try
            {
                inverseFMatch = this._foeRepo.GetByDTO(inverseF.GetRelationDTO());
            }
            catch (ArgumentException)
            { }

            if (   (fMatch == null)
                && (inverseFMatch == null)   )
            {
                try
                {
                    var r = base.Register(f);
                    this._friendRepo.EndTransaction();
                    return r;
                }
                catch (ArgumentException exc)
                {
                    this._friendRepo.DiscardTransaction();
                    throw new ArgumentException("Could not complete the registration.", exc);
                }
            }
            else
            {
                this._friendRepo.DiscardTransaction();
                throw new ArgumentException("You cannot become friends with a foe.");
            }
        }
    }
}