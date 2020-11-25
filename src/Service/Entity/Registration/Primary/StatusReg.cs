using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    public class StatusReg
        : IEntityReg<Status, Name, string>
    {
        public static readonly Status Proposed =
            new Status(new Name("Proposed"));
        public static readonly Status Active =
            new Status(new Name("Active"));
        public static readonly Status InProgress =
            new Status(new Name("In Progress"));
        public static readonly Status Retired =
            new Status(new Name("Retired"));

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