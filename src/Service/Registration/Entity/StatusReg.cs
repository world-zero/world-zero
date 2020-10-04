using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class StatusReg
        : IEntityReg<Status, Name, string>
    {
        protected IStatusRepo _statusRepo
        { get { return (IStatusRepo) this._repo; } }

        public StatusReg(IStatusRepo statusRepo)
            : base(statusRepo)
        { }
    }
}