using System;
using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class FactionRegistration
        : IEntityRegistration<Faction, Name, string>
    {
        protected readonly IAbilityRepo _abilityRepo;

        protected IFactionRepo _factionRepo
        { get { return (IFactionRepo) this._repo; } }

        public FactionRegistration(
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
            if (faction.AbilityName != null)
            {
                try
                {
                    this._abilityRepo.GetById(faction.AbilityName);
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException($"The faction {faction.Id} has an unregistered ability ({faction.AbilityName}).");
                }
            }
            return base.Register(faction);
        }
    }
}