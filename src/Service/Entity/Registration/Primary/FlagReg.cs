using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class FlagReg
        : ABCEntityReg<IFlag, Name, string>, IFlagReg
    {
        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._repo; } }

        public FlagReg(IFlagRepo flagRepo)
            : base(flagRepo)
        {
            this.EnsureExists(IFlagReg.Duplicate);
            this.EnsureExists(IFlagReg.Dangerous);
            this.EnsureExists(IFlagReg.Inappropriate);
        }

    }
}