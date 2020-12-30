using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="ILocationUpdate"/>
    public class LocationUpdate :
        ABCEntityUpdate<ILocation, Id, int>, ILocationUpdate
    {
        public LocationUpdate(ILocationRepo repo)
            : base(repo)
        { }

        // --------------------------------------------------------------------

        public void AmendCity(ILocation l, Name newCity)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newCity, "newCity");
            void f() => ((UnsafeLocation) l).City = newCity;
            this.AmendHelper(f, l);
        }

        public void AmendCity(Id locationId, Name newCity)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newCity, "newCity");
            void f()
            {
                var l = this._repo.GetById(locationId);
                this.AmendCity(l, newCity);
            }
            this.Transaction(f, true);
        }

        public async Task AmendCityAsync(ILocation l, Name newCity)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newCity, "newCity");
            await Task.Run(() => this.AmendCity(l, newCity));
        }

        public async Task AmendCityAsync(Id locationId, Name newCity)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newCity, "newCity");
            await Task.Run(() => this.AmendCity(locationId, newCity));
        }

        // --------------------------------------------------------------------

        public void AmendState(ILocation l, Name newState)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newState, "newState");
            void f() => ((UnsafeLocation) l).State = newState;
            this.AmendHelper(f, l);
        }

        public void AmendState(Id locationId, Name newState)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newState, "newState");
            void f()
            {
                var l = this._repo.GetById(locationId);
                this.AmendState(l, newState);
            }
            this.Transaction(f, true);
        }

        public async Task AmendStateAsync(ILocation l, Name newState)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newState, "newState");
            await Task.Run(() => this.AmendState(l, newState));
        }

        public async Task AmendStateAsync(Id locationId, Name newState)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newState, "newState");
            await Task.Run(() => this.AmendState(locationId, newState));
        }

        // --------------------------------------------------------------------

        public void AmendCountry(ILocation l, Name newCountry)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newCountry, "newCountry");
            void f() => ((UnsafeLocation) l).Country = newCountry;
            this.AmendHelper(f, l);
        }

        public void AmendCountry(Id locationId, Name newCountry)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newCountry, "newCountry");
            void f()
            {
                var l = this._repo.GetById(locationId);
                this.AmendCountry(l, newCountry);
            }
            this.Transaction(f, true);
        }

        public async Task AmendCountryAsync(ILocation l, Name newCountry)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newCountry, "newCountry");
            await Task.Run(() => this.AmendCountry(l, newCountry));
        }

        public async Task AmendCountryAsync(Id locationId, Name newCountry)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newCountry, "newCountry");
            await Task.Run(() => this.AmendCountry(locationId, newCountry));
        }

        // --------------------------------------------------------------------

        public void AmendZip(ILocation l, Name newZip)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newZip, "newZip");
            void f() => ((UnsafeLocation) l).Zip = newZip;
            this.AmendHelper(f, l);
        }

        public void AmendZip(Id locationId, Name newZip)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newZip, "newZip");
            void f()
            {
                var l = this._repo.GetById(locationId);
                this.AmendZip(l, newZip);
            }
            this.Transaction(f, true);
        }

        public async Task AmendZipAsync(ILocation l, Name newZip)
        {
            this.AssertNotNull(l, "l");
            this.AssertNotNull(newZip, "newZip");
            await Task.Run(() => this.AmendZip(l, newZip));
        }

        public async Task AmendZipAsync(Id locationId, Name newZip)
        {
            this.AssertNotNull(locationId, "locationId");
            this.AssertNotNull(newZip, "newZip");
            await Task.Run(() => this.AmendZip(locationId, newZip));
        }
    }
}