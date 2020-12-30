using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IIdEntity"/>
    /// <summary>
    /// Location is a entity for a tuple of the Location table.
    /// </summary>
    /// <remarks>
    /// Repos are responsible for ensuring that every combination of cities,
    /// states, countries, and zips are unique.
    /// </remarks>
    public interface ILocation : IIdEntity, IOptionalEntity
    {
        Name City { get; }
        Name State { get; }
        Name Country { get; }
        Name Zip { get; }
    }
}