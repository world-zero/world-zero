using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class LocationRegistration
        : IEntityRegistration<Location, Id, int>
    {
        protected ILocationRepo _locationRepo
        { get { return (ILocationRepo) this._repo; } }

        public LocationRegistration(ILocationRepo locationRepo)
            : base(locationRepo)
        { }
    }
}