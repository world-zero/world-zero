using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ICharacter"/>
    public sealed class Character : IUnsafeIdNamedProxy<UnsafeCharacter>, ICharacter
    {
        public Character(UnsafeCharacter character)
            : base(character)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Character(this._unsafeEntity);
        }

        public Level CalculateLevel(PointTotal points)
            => this._unsafeEntity.CalculateLevel(points);

        public bool HasBio => this._unsafeEntity.HasBio;
        public bool HasProfilePic => this._unsafeEntity.HasProfilePic;
        public Id PlayerId => this._unsafeEntity.PlayerId;
        public PointTotal VotePointsLeft => this._unsafeEntity.VotePointsLeft;
        public PointTotal EraPoints => this._unsafeEntity.EraPoints;
        public PointTotal TotalPoints => this._unsafeEntity.TotalPoints;
        public Level EraLevel => this._unsafeEntity.EraLevel;
        public Level TotalLevel => this._unsafeEntity.TotalLevel;
        public Name FactionId => this._unsafeEntity.FactionId;
        public Id LocationId => this._unsafeEntity.LocationId;
    }
}