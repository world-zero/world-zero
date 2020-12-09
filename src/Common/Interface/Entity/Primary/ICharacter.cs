using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IIdNamedEntity"/>
    /// <summary>
    /// Character is a entity for a tuple of the Character table.
    /// </summary>
    public interface ICharacter : IIdNamedEntity, IEntityHasOptional
    {
        /// <summary>
        /// Determine the level based off the number of points supplied.
        /// </summary>
        /// <param name="points">The points to calculate the level of.</param>
        /// <returns><c>Level</c> corresponding to the <c>points</c>.</returns>
        /// <remarks>
        /// This should return values corresponding to the below.
        /// <code>
        /// int p = poinnts.AsInt;
        /// if      (p < 10)   r = 0;
        /// else if (p < 70)   r = 1;
        /// else if (p < 170)  r = 2;
        /// else if (p < 330)  r = 3;
        /// else if (p < 610)  r = 4;
        /// else if (p < 1090) r = 5;
        /// else if (p < 1840) r = 6;
        /// else if (p < 3040) r = 7;
        /// else               r = 8;
        /// </code>
        /// </remarks>
        Level CalculateLevel(PointTotal points);

        /// <summary>
        /// HasBio is used to record if a character has a bio or not.
        /// </summary>
        bool HasBio { get; }

        /// <summary>
        /// HasProfilePic is used to record if a character has a profile
        /// picture or not.
        /// </summary>
        bool HasProfilePic { get; }

        Id PlayerId { get; }

        PointTotal VotePointsLeft { get; }
        PointTotal EraPoints { get; }
        PointTotal TotalPoints { get; }

        /// <summary>
        /// This property has no associated field, it returns the result of
        ///  `CalculateLevel(EraPoints)`.
        /// </summary>
        Level EraLevel { get; }

        /// <summary>
        /// This property has no associated field, it returns the result of
        ///  `CalculateLevel(TotalPoints)`.
        /// </summary>
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