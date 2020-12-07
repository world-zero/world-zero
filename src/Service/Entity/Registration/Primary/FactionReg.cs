using System;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class FactionReg
        : IEntityReg<UnsafeFaction, Name, string>
    {
        protected readonly IUnsafeAbilityRepo _abilityRepo;

        protected IUnsafeFactionRepo _factionRepo
        { get { return (IUnsafeFactionRepo) this._repo; } }

        public FactionReg(
            IUnsafeFactionRepo factionRepo,
            IUnsafeAbilityRepo abilityRepo
        )
            : base(factionRepo)
        {
            if (abilityRepo == null) throw new ArgumentNullException("abilityRepo");
            this._abilityRepo = abilityRepo;
        }

        public override UnsafeFaction Register(UnsafeFaction faction)
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