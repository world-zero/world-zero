using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IPlayerUpdate"/>
    public class PlayerUpdate
        : ABCIdNamedEntityUpdate<IPlayer>, IPlayerUpdate
    {
        public PlayerUpdate(IPlayerRepo repo)
            : base(repo)
        { }

        public void AmendIsBlocked(IPlayer p, bool newIsBlocked)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(newIsBlocked, "newIsBlocked");
            void f() => ((UnsafePlayer) p).IsBlocked = newIsBlocked;
            this.AmendHelper(f, p);
        }

        public void AmendIsBlocked(Id playerId, bool newIsBlocked)
        {
            this.AssertNotNull(playerId, "playerId");
            this.AssertNotNull(newIsBlocked, "newIsBlocked");
            void f()
            {
                var p = this._repo.GetById(playerId);
                this.AmendIsBlocked(p, newIsBlocked);
            }
            this.Transaction(f, true);
        }

        public async Task AmendIsBlockedAsync(IPlayer p, bool newIsBlocked)
        {
            this.AssertNotNull(p, "p");
            this.AssertNotNull(newIsBlocked, "newIsBlocked");
            await Task.Run(() => this.AmendIsBlocked(p, newIsBlocked));
        }

        public async Task AmendIsBlockedAsync(Id playerId, bool newIsBlocked)
        {
            this.AssertNotNull(playerId, "playerId");
            this.AssertNotNull(newIsBlocked, "newIsBlocked");
            await Task.Run(() => this.AmendIsBlocked(playerId, newIsBlocked));
        }
    }
}