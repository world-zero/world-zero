using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel"/>
    public class PlayerDel : ABCEntityDel<UnsafePlayer, Id, int>
    {
        protected IPlayerRepo _playerRepo
        { get { return  (IPlayerRepo) this._repo; } }

        protected readonly CharacterDel _charDel;

        public PlayerDel(IPlayerRepo playerRepo, CharacterDel charDel)
            : base(playerRepo)
        {
            this.AssertNotNull(charDel, "charDel");
            this._charDel = charDel;
        }

        public override void Delete(Id playerId)
        {
            this.AssertNotNull(playerId, "playerId");
            void f(Id id)
            {
                this._charDel.DeleteByPlayer(id);
                base.Delete(id);
            }
            this.Transaction<Id>(f, playerId);
        }
    }
}