using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IIdNamedEntity"/>
    /// <summary>
    /// Player is a entity for a tuple of the Player table.
    /// </summary>
    public interface IPlayer : IIdNamedEntity
    {
        /// <summary>
        /// IsBlocked controls whether or not a Player can sign into any of
        /// their characters.
        /// </summary>
        bool IsBlocked { get; }
    }
}