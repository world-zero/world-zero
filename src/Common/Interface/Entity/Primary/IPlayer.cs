using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IPlayerDTO"/>
    public interface IPlayer : IIdNamedEntity
    {
        bool IsBlocked { get; }
    }
}