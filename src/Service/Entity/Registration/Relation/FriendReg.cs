using System;
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
    /// <remarks>
    /// This will not allow friends to be foes.
    /// </remarks>
    public class FriendReg
        : IEntityRelationReg
        <
            UnsafeFriend,
            UnsafeCharacter,
            Id,
            int,
            UnsafeCharacter,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        protected IUnsafeFriendRepo _friendRepo
        { get { return (IUnsafeFriendRepo) this._repo; } }

        protected readonly IUnsafeFoeRepo _foeRepo;

        public FriendReg(
            IUnsafeFriendRepo friendRepo,
            IUnsafeCharacterRepo characterRepo,
            IUnsafeFoeRepo foeRepo
        )
            : base(friendRepo, characterRepo, characterRepo)
        {
            this._foeRepo = foeRepo;
        }

        public override UnsafeFriend Register(UnsafeFriend f)
        {
            UnsafeFriend inverseF = new UnsafeFriend(f.Id, f.RightId, f.LeftId);

            UnsafeFoe fMatch = null;
            UnsafeFoe inverseFMatch = null;

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