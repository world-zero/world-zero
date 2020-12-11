using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class StatusReg
        : IEntityReg<UnsafeStatus, Name, string>
    {
        public static readonly UnsafeStatus Proposed =
            new UnsafeStatus(new Name("Proposed"));
        public static readonly UnsafeStatus Active =
            new UnsafeStatus(new Name("Active"));
        public static readonly UnsafeStatus InProgress =
            new UnsafeStatus(new Name("In Progress"));
        public static readonly UnsafeStatus Retired =
            new UnsafeStatus(new Name("Retired"));

        protected IStatusRepo _statusRepo
        { get { return (IStatusRepo) this._repo; } }

        public StatusReg(IStatusRepo statusRepo)
            : base(statusRepo)
        {
            this.EnsureExists(StatusReg.Proposed);
            this.EnsureExists(StatusReg.Active);
            this.EnsureExists(StatusReg.InProgress);
            this.EnsureExists(StatusReg.Retired);
        }
    }
}