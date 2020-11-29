using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class PlayerReg
        : IEntityReg<Player, Id, int>
    {
        protected IPlayerRepo _playerRepo
        { get { return (IPlayerRepo) this._repo; } }

        public PlayerReg(IPlayerRepo playerRepo)
            : base(playerRepo)
        { }
    }
}