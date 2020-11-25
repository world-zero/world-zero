namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <summary>
    /// This marker is used to signify that an entity is optional.
    /// </summary>
    /// <remarks>
    /// An entity is optional if another entity has a zero-to-one relation to
    /// entities with this marker. An example of an entity with this marker
    /// would be Ability, which Faction(s) may have one or none of. This means
    /// that Faction's relation to Ability is Optional. To meet this
    /// requirement, an entity just needs at least one zero-to-one relations,
    /// regardless of any other relations it has.
    /// <br />
    /// This is the inverse of marker <see
    /// cref="WorldZero.Common.Interface.Entity.Generic.Primary.IEntityHasOptional"/>.
    /// </remarks>
    public interface IOptionalEntity
    { }
}