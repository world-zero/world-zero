using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <remarks>
    /// FactionId and LocationId can be null.
    /// <br />
    /// The *Level properties are computed dynamically via
    /// <see cref="Level.CalculateLevel(PointTotal)"/>.
    /// </remarks>
    /// <inheritdoc cref="ICharacterDTO"/>
    public interface ICharacter :
        ICharacterDTO,
        IIdNamedEntity,
        IEntityHasOptional
    { }
}