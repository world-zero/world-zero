using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Constant.Entity.Primary;

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
            new ConstantStatuses(statusRepo).EnsureEntitiesExist();
        }
    }
}