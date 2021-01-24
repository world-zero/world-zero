using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Unspecified.Primary
{
    /// <inheritdoc cref="IIdEntity"/>
    /// <remarks>
    /// This class exists to have entities that have an `Id` ID property, and
    /// also have a required name property - critically, this name must be
    /// unique. As with this type of rule, uniqueness should be enforced by the
    /// repo. This name can only be valid.
    /// </remarks>
    public interface IIdNamedEntity : IIdNamedDTO, IIdEntity
    { }
}