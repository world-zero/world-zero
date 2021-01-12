using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary
{
    /// <summary>
    /// This is the abstraction for an entity DTO, which has no creation
    /// restrictions, but is rarely, if ever, used in system services.
    /// </summary>
    public interface IEntityDTO<TId, TBuiltIn> : IDTO
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        TId Id { get; }
    }
}