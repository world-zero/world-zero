using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Constant.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IFactionUpdate"/>
    public class FactionUpdate
        : ABCEntityUpdate<IFaction, Name, string>, IFactionUpdate
    {
        protected readonly IAbilityRepo _abilityRepo;

        public FactionUpdate(IFactionRepo repo, IAbilityRepo abilityRepo)
            : base(repo)
        {
            this.AssertNotNull(abilityRepo, "abilityRepo");
            this._abilityRepo = abilityRepo;
        }

        // --------------------------------------------------------------------

        public void AmendDescription(IFaction f, string newDesc)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newDesc, "newDesc");
            void g() => ((UnsafeFaction) f).Description = newDesc;
            this.AmendHelper(g, f);
        }

        public void AmendDescription(Name factionId, string newDesc)
        {
            this.AssertNotNull(factionId, "factionId");
            this.AssertNotNull(newDesc, "newDesc");
            void g()
            {
                var f = this._repo.GetById(factionId);
                this.AmendDescription(f, newDesc);
            }
            this.Transaction(g, true);
        }

        public async
        Task AmendDescriptionAsync(IFaction f, string newDesc)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() =>
                this.AmendDescription(f, newDesc));
        }

        public async
        Task AmendDescriptionAsync(Name factionId, string newDesc)
        {
            this.AssertNotNull(factionId, "factionId");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() =>
                this.AmendDescription(factionId, newDesc));
        }

        // --------------------------------------------------------------------
        
        public void AmendDateFounded(IFaction f, PastDate newDateFounded)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newDateFounded, "newDateFounded");
            void g() => ((UnsafeFaction) f).DateFounded = newDateFounded;
            this.AmendHelper(g, f);
        }

        public void AmendDateFounded(Name factionId, PastDate newDateFounded)
        {
            this.AssertNotNull(factionId, "factionId");
            this.AssertNotNull(newDateFounded, "newDateFounded");
            void g()
            {
                var f = this._repo.GetById(factionId);
                this.AmendDateFounded(f, newDateFounded);
            }
            this.Transaction(g, true);
        }

        public async
        Task AmendDateFoundedAsync(IFaction f, PastDate newDateFounded)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newDateFounded, "newDateFounded");
            await Task.Run(() =>
                this.AmendDateFounded(f, newDateFounded));
        }

        public async
        Task AmendDateFoundedAsync(Name factionId, PastDate newDateFounded)
        {
            this.AssertNotNull(factionId, "factionId");
            this.AssertNotNull(newDateFounded, "newDateFounded");
            await Task.Run(() =>
                this.AmendDateFounded(factionId, newDateFounded));
        }

        // --------------------------------------------------------------------

        private Name _getAbilityId(IAbility ability)
        {
            if (ability == null)
                return null;

            foreach (IAbility a in ConstantAbilities.StaticGetEntities())
            {
                if (ability.Id == a.Id)
                    return ability.Id;
            }
            this._abilityRepo.GetById(ability.Id);
            return ability.Id;
        }

        private Name _getAbilityId(Name abilityId)
        {
            if (abilityId == null)
                return null;

            foreach (IAbility a in ConstantAbilities.StaticGetEntities())
            {
                if (a.Id == abilityId)
                    return abilityId;
            }
            this._abilityRepo.GetById(abilityId);
            return abilityId;
        }

        public void AmendAbility(IFaction f, IAbility newAbility)
        {
            this.AssertNotNull(f, "f");
            void g()
            {
                ((UnsafeFaction) f).AbilityId =
                    this._getAbilityId(newAbility);
            }
            this.AmendHelper(g, f, true);
        }

        public void AmendAbility(IFaction f, Name newAbilityId)
        {
            this.AssertNotNull(f, "f");
            void g()
            {
                ((UnsafeFaction) f).AbilityId =
                    this._getAbilityId(newAbilityId);
            }
            this.AmendHelper(g, f, true);
        }

        public void AmendAbility(Name factionId, IAbility newAbility)
        {
            this.AssertNotNull(factionId, "factionId");
            void g()
            {
                var f = this._repo.GetById(factionId);
                this.AmendAbility(f, newAbility);
            }
            this.Transaction(g, true);
        }

        public void AmendAbility(Name factionId, Name newAbilityId)
        {
            this.AssertNotNull(factionId, "factionId");
            void g()
            {
                var f = this._repo.GetById(factionId);
                this.AmendAbility(f, newAbilityId);
            }
            this.Transaction(g, true);
        }

        public async
        Task AmendAbilityAsync(IFaction f, IAbility newAbility)
        {
            this.AssertNotNull(f, "f");
            await Task.Run(() =>
                this.AmendAbility(f, newAbility));
        }

        public async
        Task AmendAbilityAsync(IFaction f, Name newAbilityId)
        {
            this.AssertNotNull(f, "f");
            await Task.Run(() =>
                this.AmendAbility(f, newAbilityId));
        }

        public async
        Task AmendAbilityAsync(Name factionId, IAbility newAbility)
        {
            this.AssertNotNull(factionId, "factionId");
            await Task.Run(() =>
                this.AmendAbility(factionId, newAbility));
        }

        public async
        Task AmendAbilityAsync(Name factionId, Name newAbilityId)
        {
            this.AssertNotNull(factionId, "factionId");
            await Task.Run(() =>
                this.AmendAbility(factionId, newAbilityId));
        }
    }
}