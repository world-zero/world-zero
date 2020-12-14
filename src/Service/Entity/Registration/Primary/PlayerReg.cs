using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IPlayerReg"/>
    public class PlayerReg
        : ABCEntityReg<IPlayer, Id, int>, IPlayerReg
    {
        protected IPlayerRepo _playerRepo
        { get { return (IPlayerRepo) this._repo; } }

        public PlayerReg(IPlayerRepo playerRepo)
            : base(playerRepo)
        { }
    }
}