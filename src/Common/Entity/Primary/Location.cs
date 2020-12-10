using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ILocation"/>
    public sealed class Location : IUnsafeIdProxy<UnsafeLocation>, ILocation
    {
        public Location(UnsafeLocation location)
            : base(location)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Location(this._unsafeEntity);
        }

        public Name City => this._unsafeEntity.City;
        public Name State => this._unsafeEntity.State;
        public Name Country => this._unsafeEntity.Country;
        public Name Zip => this._unsafeEntity.Zip;
    }
}