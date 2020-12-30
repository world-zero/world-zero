using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface ILocationUpdate : IEntityUpdate<ILocation, Id, int>
    {
        void AmendCity(ILocation l, Name newCity);
        void AmendCity(Id locationId, Name newCity);
        Task AmendCityAsync(ILocation l, Name newCity);
        Task AmendCityAsync(Id locationId, Name newCity);

        void AmendState(ILocation l, Name newState);
        void AmendState(Id locationId, Name newState);
        Task AmendStateAsync(ILocation l, Name newState);
        Task AmendStateAsync(Id locationId, Name newState);

        void AmendCountry(ILocation l, Name newCountry);
        void AmendCountry(Id locationId, Name newCountry);
        Task AmendCountryAsync(ILocation l, Name newCountry);
        Task AmendCountryAsync(Id locationId, Name newCountry);

        void AmendZip(ILocation l, Name newZip);
        void AmendZip(Id locationId, Name newZip);
        Task AmendZipAsync(ILocation l, Name newZip);
        Task AmendZipAsync(Id locationId, Name newZip);
    }
}