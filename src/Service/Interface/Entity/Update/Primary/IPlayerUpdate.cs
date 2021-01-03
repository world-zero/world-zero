using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IIdNamedEntityUpdate{TEntity}"/>
    public interface IPlayerUpdate : IIdNamedEntityUpdate<IPlayer>
    {
        void AmendIsBlocked(IPlayer p, bool newIsBlocked);
        void AmendIsBlocked(Id playerId, bool newIsBlocked);
        Task AmendIsBlockedAsync(IPlayer p, bool newIsBlocked);
        Task AmendIsBlockedAsync(Id playerId, bool newIsBlocked);
    }
}