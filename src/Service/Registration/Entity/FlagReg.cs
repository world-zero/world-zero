using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class FlagReg
        : IEntityReg<Flag, Name, string>
    {
        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._repo; } }

        public FlagReg(IFlagRepo flagRepo)
            : base(flagRepo)
        { }
    }
}