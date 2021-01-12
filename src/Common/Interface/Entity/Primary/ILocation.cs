using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="ILocationDTO"/>
    public interface ILocation : IIdEntity, IOptionalEntity
    {
        Name City { get; }
        Name State { get; }
        Name Country { get; }
        Name Zip { get; }
    }
}