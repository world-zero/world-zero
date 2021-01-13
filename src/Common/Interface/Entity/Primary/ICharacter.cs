using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="ICharacterDTO"/>
    public interface ICharacter : IIdNamedEntity, IEntityHasOptional
    {
        bool HasBio { get; }

        bool HasProfilePic { get; }

        Id PlayerId { get; }

        PointTotal VotePointsLeft { get; }
        PointTotal EraPoints { get; }
        PointTotal TotalPoints { get; }

        Level EraLevel { get; }

        Level TotalLevel { get; }

        /// <summary>
        /// This value can be null.
        /// </summary>
        Name FactionId { get; }

        /// <summary>
        /// This value can be null.
        /// </summary>
        Id LocationId { get; }
    }
}