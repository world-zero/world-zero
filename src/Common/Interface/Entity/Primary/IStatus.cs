using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IStatusDTO"/>
    public interface IStatus : INamedEntity
    {
        /// <summary>
        /// This can be `null`.
        /// </summary>
        string Description { get; }
    }
}