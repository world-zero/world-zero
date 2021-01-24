using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Character is a entity for a tuple of the Character table.
    /// </summary>
    public interface ICharacterDTO : IIdNamedDTO
    {
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
        Level EraLevel { get; }
        Level TotalLevel { get; }
        Name FactionId { get; }
        Id LocationId { get; }
    }
}