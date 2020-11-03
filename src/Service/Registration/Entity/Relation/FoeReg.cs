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
    public class FoeReg
        : IEntityRelationReg
        <
            Foe,
            Character,
            Id,
            int,
            Character,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
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

        public override Foe Register(Foe f)
        {
            this.PreRegisterChecks(f, "f");
            Foe inverseF = new Foe(f.Id, f.RightId, f.LeftId);

            Friend fMatch = null;
            Friend inverseFMatch = null;

            this._friendRepo.BeginTransaction(true);
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
                var r = base.Register(f);
                this._friendRepo.EndTransaction();
                return r;
            }
            else
            {
                this._friendRepo.DiscardTransaction();
                throw new ArgumentException("You cannot become foes with a friend.");
            }
        }
    }
}