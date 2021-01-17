using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="ILocationDTO"/>
    public class LocationDTO : EntityDTO<Id, int>, ILocationDTO
    {
        public Name City { get; }
        public Name State { get; }
        public Name Country { get; }
        public Name Zip { get; }

        public LocationDTO(
            Id id=null,
            Name city=null,
            Name state=null,
            Name country=null,
            Name zip=null
        )
            : base(id)
        {
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Zip = zip;
        }

        public override object Clone()
        {
            return new LocationDTO(
                this.Id,
                this.City,
                this.State,
                this.Country,
                this.Zip
            );
        }

        public override bool Equals(IDTO dto)
        {
            var l = dto as LocationDTO;
            return
                l != null
                && base.Equals(l)
                && this.XOR(this.City, l.City)
                && this.XOR(this.State, l.State)
                && this.XOR(this.Country, l.Country)
                && this.XOR(this.Zip, l.Zip);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var r = base.GetHashCode();
                if (this.City != null) r *= this.City.GetHashCode();
                if (this.State != null) r *= this.State.GetHashCode();
                if (this.Country != null) r *= this.Country.GetHashCode();
                if (this.Zip != null) r *= this.Zip.GetHashCode();
                return r;
            }
        }
    }
}