using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface IAbilityUpdate : IEntityUpdate<IAbility, Name, string>
    {
        void AmendDescription(IAbility a, string newDesc);
        void AmendDescription(Name abilityId, string newDesc);
        Task AmendDescriptionAsync(IAbility a, string newDesc);
        Task AmendDescriptionAsync(Name abilityId, string newDesc);
    }
}