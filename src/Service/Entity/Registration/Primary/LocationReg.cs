using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="ILocationReg"/>
    public class LocationReg
        : ABCEntityReg<ILocation, Id, int>, ILocationReg
    {
        protected ILocationRepo _locationRepo
        { get { return (ILocationRepo) this._repo; } }

        public LocationReg(ILocationRepo locationRepo)
            : base(locationRepo)
        { }
    }
}