using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Location is a entity for a tuple of the Location table.
    /// </summary>
    /// <remarks>
    /// Repos are responsible for ensuring that every combination of cities,
    /// states, countries, and zips are unique.
    /// </remarks>
    public interface ILocationDTO : IEntityDTO<Id, int>
    {
        Name City { get; }
        Name State { get; }
        Name Country { get; }
        Name Zip { get; }
    }
}