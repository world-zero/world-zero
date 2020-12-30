using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface IFactionUpdate : IEntityUpdate<IFaction, Name, string>
    {
        void AmendDescription(IFaction f, string newDesc);
        void AmendDescription(Name factionId, string newDesc);
        Task AmendDescriptionAsync(IFaction f, string newDesc);
        Task AmendDescriptionAsync(Name factionId, string newDesc);

        void AmendDateFounded(IFaction f, PastDate newDateFounded);
        void AmendDateFounded(Name factionId, PastDate newDateFounded);
        Task AmendDateFoundedAsync(IFaction f, PastDate newDateFounded);
        Task AmendDateFoundedAsync(Name factionId, PastDate newDateFounded);

        void AmendAbility(IFaction f, IAbility newAbility);
        void AmendAbility(IFaction f, Name newAbilityId);
        void AmendAbility(Name factionId, IAbility newAbility);
        void AmendAbility(Name factionId, Name newAbilityId);
        Task AmendAbilityAsync(IFaction f, IAbility newAbility);
        Task AmendAbilityAsync(IFaction f, Name newAbilityId);
        Task AmendAbilityAsync(Name factionId, IAbility newAbility);
        Task AmendAbilityAsync(Name factionId, Name newAbilityId);
    }
}