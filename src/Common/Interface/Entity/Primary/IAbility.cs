using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IAbilityDTO"/>
    public interface IAbility : INamedEntity, IOptionalEntity
    {
        string Description { get; }
    }
}