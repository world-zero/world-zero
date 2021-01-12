using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Tag is a entity for a tuple of the Tag table.
    /// </summary>
    public interface ITagDTO : IEntityDTO<Name, string>
    {
        string Description { get; }
    }
}