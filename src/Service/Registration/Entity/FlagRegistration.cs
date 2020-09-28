using WorldZero.Service.Interface.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class FlagRegistration
        : IEntityRegistration<Flag, Name, string>
    {
        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._repo; } }

        public FlagRegistration(IFlagRepo flagRepo)
            : base(flagRepo)
        { }
    }
}