using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class PlayerReg
        : ABCEntityReg<UnsafePlayer, Id, int>
    {
        protected IPlayerRepo _playerRepo
        { get { return (IPlayerRepo) this._repo; } }

        public PlayerReg(IPlayerRepo playerRepo)
            : base(playerRepo)
        { }
    }
}