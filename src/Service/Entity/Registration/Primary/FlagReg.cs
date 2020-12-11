using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class FlagReg
        : IEntityReg<UnsafeFlag, Name, string>
    {
        public static readonly UnsafeFlag Duplicate =
            new UnsafeFlag(new Name("Duplicate"));
        public static readonly UnsafeFlag Dangerous =
            new UnsafeFlag(new Name("Dangerous"));
        public static readonly UnsafeFlag Inappropriate =
            new UnsafeFlag(new Name("Inappropriate"));

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