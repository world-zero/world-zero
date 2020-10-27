using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class FlagReg
        : IEntityReg<Flag, Name, string>
    {
        public static readonly Flag Duplicate =
            new Flag(new Name("Duplicate"));
        public static readonly Flag Dangerous =
            new Flag(new Name("Dangerous"));
        public static readonly Flag Inappropriate =
            new Flag(new Name("Inappropriate"));

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._repo; } }

        public FlagReg(IFlagRepo flagRepo)
            : base(flagRepo)
        {
            this.EnsureExists(FlagReg.Duplicate);
            this.EnsureExists(FlagReg.Dangerous);
            this.EnsureExists(FlagReg.Inappropriate);
        }

    }
}