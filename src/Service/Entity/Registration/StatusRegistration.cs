using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class StatusRegistration
        : IEntityRegistration<Status, Name, string>
    {
        protected IStatusRepo _statusRepo
        { get { return (IStatusRepo) this._repo; } }

        public StatusRegistration(IStatusRepo statusRepo)
            : base(statusRepo)
        { }
    }
}