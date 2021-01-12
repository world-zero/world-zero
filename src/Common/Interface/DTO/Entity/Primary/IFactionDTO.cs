using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Faction is a entity for a tuple of the Faction table.
    /// </summary>
    public interface IFactionDTO : IEntityDTO<Name, string>
    {
        string Description { get; }
        PastDate DateFounded { get; }
        Name AbilityId { get; }
    }
}