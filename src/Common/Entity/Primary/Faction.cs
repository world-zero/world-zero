using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IFaction"/>
    public sealed class Faction : IUnsafeNamedProxy<UnsafeFaction>, IFaction
    {
        public Faction(UnsafeFaction faction)
            : base(faction)
        { }

        public override IEntity<Name, string> Clone()
        {
            return new Faction(this._unsafeEntity);
        }

        public PastDate DateFounded => this._unsafeEntity.DateFounded;
        public Name AbilityId => this._unsafeEntity.AbilityId;
        public string Description => this._unsafeEntity.Description;
    }
}