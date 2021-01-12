using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary
{
    /// <summary>
    /// This extension has a name in addition to an <see cref="Id"/> ID.
    /// </summary>
    public interface IIdNamedDTO : IEntityDTO<Id, int>
    {
        Name Name { get; }
    }
}