using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Status is a entity for a tuple of the Status table.
    /// </summary>
    public interface IStatusDTO : IEntityDTO<Name, string>
    {
        string Description { get; }
    }
}