using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="INamedEntity"/>
    /// <summary>
    /// An ability is something that faction(s) can have to give them some
    /// bonus.
    /// </summary>
    public interface IAbility : INamedEntity, IOptionalEntity
    {
        string Description { get; }
    }
}