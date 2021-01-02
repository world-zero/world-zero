using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="INamedEntity"/>
    /// <summary>
    /// Status is a entity for a tuple of the Status table.
    /// </summary>
    public interface IStatus : INamedEntity
    {
        /// <summary>
        /// This can be `null`.
        /// </summary>
        string Description { get; }
    }
}