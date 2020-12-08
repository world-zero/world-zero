using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Location is a entity for a tuple of the Location table.
    /// </summary>
    /// <remarks>
    /// Repos are responsible for ensuring that every combination of cities,
    /// states, countries, and zips are unique.
    /// </remarks>
    public class UnsafeLocation : ABCIdEntity, IOptionalEntity, IUnsafeEntity
    {
        public UnsafeLocation(
            Name city,
            Name state,
            Name country,
            Name zip
        )
            : base()
        {
            this._setup(
                city,
                state,
                country,
                zip
            );
        }

        public UnsafeLocation(
            Id id,
            Name city,
            Name state,
            Name country,
            Name zip
        )
            : base(id)
        {
            this._setup(
                city,
                state,
                country,
                zip
            );
        }

        internal UnsafeLocation(
            int id,
            string city,
            string state,
            string country,
            string zip
        )
            : base(new Id(id))
        {
            this._setup(
                new Name(city),
                new Name(state),
                new Name(country),
                new Name(zip)
            );
        }

        private void _setup(Name city, Name state, Name country, Name zip)
        {
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Zip = zip;
        }

        public override ABCEntity<Id, int> Clone()
        {
            return new UnsafeLocation(
                this.Id,
                this.City,
                this.State,
                this.Country,
                this.Zip
            );
        }

        public Name City
        {
            get { return this._city; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("City");
                this._city = value;
            }
        }
        private Name _city;

        public Name State
        {
            get { return this._state; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("State");
                this._state = value;
            }
        }
        private Name _state;

        public Name Country
        {
            get { return this._country; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Country");
                this._country = value;
            }
        }
        private Name _country;

        public Name Zip
        {
            get { return this._zip; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Zip");
                this._zip = value;
            }
        }
        private Name _zip;
    }
}