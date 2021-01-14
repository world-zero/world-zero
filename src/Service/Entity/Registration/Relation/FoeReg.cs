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
    /// <inheritdoc cref="IFoeReg"/>
    public class FoeReg
        : ABCEntityRelationReg
        <
            IFoe,
            ICharacter,
            Id,
            int,
            ICharacter,
            Id,
            int,
            NoIdRelationDTO<Id, int, Id, int>
        >, IFoeReg
    {
        protected IFoeRepo _foeRepo
        { get { return (IFoeRepo) this._repo; } }

        protected readonly IFriendRepo _friendRepo;

        public FoeReg(
            IFoeRepo foeRepo,
            ICharacterRepo characterRepo,
            IFriendRepo friendRepo
        )
            : base(foeRepo, characterRepo, characterRepo)
        {
            this.AssertNotNull(friendRepo, "friendRepo");
            this._friendRepo = friendRepo;
        }

        public override IFoe Register(IFoe f)
        {
            this.AssertNotNull(f, "f");
            IFoe inverseF = new UnsafeFoe(f.Id, f.RightId, f.LeftId);

            IFriend fMatch = null;
            IFriend inverseFMatch = null;

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
                fMatch = this._friendRepo.GetByDTO(f.GetRelationDTO());
            }
            catch (ArgumentException)
            { }
            try
            {
                inverseFMatch = this._friendRepo.GetByDTO(inverseF.GetRelationDTO());
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