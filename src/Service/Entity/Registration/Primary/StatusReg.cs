using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Registration.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IStatusReg"/>
    public class StatusReg
        : ABCEntityReg<IStatus, Name, string>, IStatusReg
    {
        protected IStatusRepo _statusRepo
        { get { return (IStatusRepo) this._repo; } }

        public StatusReg(IStatusRepo statusRepo)
            : base(statusRepo)
        {
            this.EnsureExists(IStatusReg.Proposed);
            this.EnsureExists(IStatusReg.Active);
            this.EnsureExists(IStatusReg.InProgress);
            this.EnsureExists(IStatusReg.Retired);
        }
    }
}