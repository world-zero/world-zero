using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <remarks>
    /// Description can be null.
    /// </remarks>
    /// <inheritdoc cref="ITagDTO"/>
    public interface ITag : ITagDTO, INamedEntity
    { }
}