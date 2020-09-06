using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
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