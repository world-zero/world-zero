using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="INamedEntity"/>
    /// <summary>
    /// Tag is a entity for a tuple of the Tag table.
    /// </summary>
    public interface ITag : INamedEntity
    {
        /// <summary>
        /// This can be `null`.
        /// </summary>
        string Description { get; }
    }
}