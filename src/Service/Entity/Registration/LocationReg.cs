using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class LocationReg
        : IEntityReg<Location, Id, int>
    {
        protected ILocationRepo _locationRepo
        { get { return (ILocationRepo) this._repo; } }

        public LocationReg(ILocationRepo locationRepo)
            : base(locationRepo)
        { }
    }
}