using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Location is a entity for a tuple of the Location table.
    /// </summary>
    /// <remarks>
    /// Repos are responsible for ensuring that every combination of cities,
    /// states, countries, and zips are unique.
    /// </remarks>
    public class Location : IIdEntity
    {
        public Location(Name city, Name state, Name country, Name zip)
            : base()
        {
            this._setup(
                city,
                state,
                country,
                zip
            );
        }

        public Location(Id id, Name city, Name state, Name country, Name zip)
            : base(id)
        {
            this._setup(
                city,
                state,
                country,
                zip
            );
        }

        internal Location(int id, string city, string state, string country, string zip)
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

        public Name City
        {
            get { return this._city; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Cannot have a NULL city name.");
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
                    throw new ArgumentException("Cannot have a NULL state name.");
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
                    throw new ArgumentException("Cannot have a NULL country name.");
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
                    throw new ArgumentException("Cannot have a NULL zip name.");
                this._zip = value;
            }
        }
        private Name _zip;
    }
}