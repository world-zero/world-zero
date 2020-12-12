using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class LocationReg
        : ABCEntityReg<UnsafeLocation, Id, int>
    {
        protected ILocationRepo _locationRepo
        { get { return (ILocationRepo) this._repo; } }

        public LocationReg(ILocationRepo locationRepo)
            : base(locationRepo)
        { }
    }
}