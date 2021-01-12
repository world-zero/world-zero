using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary
{
    /// <summary>
    /// This extension has a Status ID in addition to an <see cref="Id"/> ID.
    /// </summary>
    public interface IIdStatusedDTO : IEntityDTO<Id, int>
    {
        Name StatusId { get; }
    }
}