using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.DTO.Entity.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IAbilityDTO"/>
    public interface IAbility :
        IAbilityDTO,
        INamedEntity,
        IOptionalEntity
    { }
}