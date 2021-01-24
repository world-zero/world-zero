using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <remarks>
    /// AbilityId can be null.
    /// </remarks>
    /// <inheritdoc cref="IFactionDTO"/>
    public interface IFaction :
        IFactionDTO,
        INamedEntity,
        IOptionalEntity,
        IEntityHasOptional
    { }
}