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
    public class FoeReg
        : IEntityRelationReg
        <
            UnsafeFoe,
            UnsafeCharacter,
            Id,
            int,
            UnsafeCharacter,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    {
        protected IUnsafeFoeRepo _foeRepo
        { get { return (IUnsafeFoeRepo) this._repo; } }

        protected readonly IUnsafeFriendRepo _friendRepo;

        public FoeReg(
            IUnsafeFoeRepo foeRepo,
            IUnsafeCharacterRepo characterRepo,
            IUnsafeFriendRepo friendRepo
        )
            : base(foeRepo, characterRepo, characterRepo)
        {
            this.AssertNotNull(friendRepo, "friendRepo");
            this._friendRepo = friendRepo;
        }

        public override UnsafeFoe Register(UnsafeFoe f)
        {
            this.AssertNotNull(f, "f");
            UnsafeFoe inverseF = new UnsafeFoe(f.Id, f.RightId, f.LeftId);

            UnsafeFriend fMatch = null;
            UnsafeFriend inverseFMatch = null;

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
                fMatch = this._friendRepo.GetByDTO(f.GetDTO());
            }
            catch (ArgumentException)
            { }
            try
            {
                inverseFMatch = this._friendRepo.GetByDTO(inverseF.GetDTO());
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
                throw new ArgumentException("You cannot become foes with a friend.");
            }
        }
    }
}