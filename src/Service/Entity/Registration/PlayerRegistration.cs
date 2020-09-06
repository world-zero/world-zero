using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class PlayerRegistration
        : IEntityRegistration<Player, Id, int>
    {
        protected IPlayerRepo _playerRepo
        { get { return (IPlayerRepo) this._repo; } }

        public PlayerRegistration(IPlayerRepo playerRepo)
            : base(playerRepo)
        { }
    }
}