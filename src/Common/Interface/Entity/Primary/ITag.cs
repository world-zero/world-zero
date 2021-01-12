using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="ITagDTO"/>
    public interface ITag : INamedEntity
    {
        /// <summary>
        /// This can be `null`.
        /// </summary>
        string Description { get; }
    }
}