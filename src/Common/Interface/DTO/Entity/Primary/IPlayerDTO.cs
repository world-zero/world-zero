using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Player is a entity for a tuple of the Player table.
    /// </summary>
    public interface IPlayerDTO : IIdNamedDTO
    {
        /// <summary>
        /// IsBlocked controls whether or not a Player can sign into any of
        /// their characters.
        /// </summary>
        bool IsBlocked { get; }
    }
}