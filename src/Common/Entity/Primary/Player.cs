using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IPlayer"/>
    public sealed class Player
        : IUnsafeIdNamedProxy<UnsafePlayer>, IPlayer
    {
        public Player(UnsafePlayer player)
            : base(player)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Player(this._unsafeEntity);
        }

        public bool IsBlocked => this._unsafeEntity.IsBlocked;
    }
}