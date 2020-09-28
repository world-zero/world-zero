using WorldZero.Service.Interface.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
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