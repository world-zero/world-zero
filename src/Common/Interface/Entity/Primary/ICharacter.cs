using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="ICharacterDTO"/>
    public interface ICharacter : IIdNamedEntity, IEntityHasOptional
    {
        /// <summary>
        /// Determine the level based off the number of points supplied.
        /// </summary>
        /// <param name="points">The points to calculate the level of.</param>
        /// <returns><c>Level</c> corresponding to the <c>points</c>.</returns>
        Level CalculateLevel(PointTotal points);

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