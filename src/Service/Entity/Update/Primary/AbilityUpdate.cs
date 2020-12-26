using System;
using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IAbilityUpdate"/>
    public class AbilityUpdate
        : ABCEntityUpdate<IAbility, Name, string>,
        IAbilityUpdate
    {
        public AbilityUpdate(IAbilityRepo repo)
            : base(repo)
        { }

        public void AmendDescription(IAbility a, string newDesc)
        {
            this.AssertNotNull(a, "a");
            this.AssertNotNull(newDesc, "newDesc");
            void f()
            {
                ((UnsafeAbility) a).Description = newDesc;
            }
            this.AmendHelper<IAbility>(f, a);
        }

        public void AmendDescription(Name abilityId, string newDesc)
        {
            this.AssertNotNull(abilityId, "abilityId");
            this.AssertNotNull(newDesc, "newDesc");
            void f()
            {
                this.AmendDescription(this._repo.GetById(abilityId), newDesc);
            }
            this.Transaction(f, true);
        }

        public async Task AmendDescriptionAsync(IAbility a, string newDesc)
        {
            this.AssertNotNull(a, "a");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(a, newDesc));
        }

        public async Task AmendDescriptionAsync(Name abilityId, string newDesc)
        {
            this.AssertNotNull(abilityId, "abilityId");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescriptionAsync(abilityId, newDesc));
        }
    }
}