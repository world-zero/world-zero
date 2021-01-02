using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="INamedEntity"/>
    /// <summary>
    /// Faction is a entity for a tuple of the Faction table.
    /// </summary>
    public interface IFaction
        : INamedEntity, IOptionalEntity, IEntityHasOptional
    {
        string Description { get; }

        PastDate DateFounded { get; }

        /// <remarks>
        /// This can be `null`.
        /// </remarks>
        Name AbilityId { get; }
    }
}