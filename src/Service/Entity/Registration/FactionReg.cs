using System;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class FactionReg
        : IEntityReg<Faction, Name, string>
    {
        protected readonly IAbilityRepo _abilityRepo;

        protected IFactionRepo _factionRepo
        { get { return (IFactionRepo) this._repo; } }

        public FactionReg(
            IFactionRepo factionRepo,
            IAbilityRepo abilityRepo
        )
            : base(factionRepo)
        {
            if (abilityRepo == null) throw new ArgumentNullException("abilityRepo");
            this._abilityRepo = abilityRepo;
        }

        public override Faction Register(Faction faction)
        {
            this._factionRepo.BeginTransaction(true);
            if (faction.AbilityId != null)
            {
                try
                {
                    this._abilityRepo.GetById(faction.AbilityId);
                }
                catch (ArgumentException)
                {
                    this._factionRepo.DiscardTransaction();
                    throw new ArgumentException($"The faction {faction.Id.Get} has an unregistered ability ({faction.AbilityId.Get}).");
                }
            }
            try
            {
                var r = base.Register(faction);
                this._factionRepo.EndTransaction();
                return r;
            }
            catch (ArgumentException exc)
            {
                this._factionRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }
    }
}