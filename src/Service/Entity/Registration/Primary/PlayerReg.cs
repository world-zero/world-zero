using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class PlayerReg
        : IEntityReg<UnsafePlayer, Id, int>
    {
        protected IUnsafePlayerRepo _playerRepo
        { get { return (IUnsafePlayerRepo) this._repo; } }

        public PlayerReg(IUnsafePlayerRepo playerRepo)
            : base(playerRepo)
        { }
    }
}