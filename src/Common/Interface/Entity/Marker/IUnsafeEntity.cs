namespace WorldZero.Common.Interface.Entity.Marker
{
    /// <summary>
    /// This marker signifies that the corresponding entity is unsafe. This
    /// means that the entity has no set property restrictions (unless a
    /// specific property does not desire to expose it). These entities are
    /// necessary from a deep back-end perspective, but they offer too much
    /// flexibility as they reach closer to the top of the project stack. To
    /// address this issue, all entities now have abstract parents which unsafe
    /// entities will implement, and then be proxied by a more restrictive
    /// implementation.
    /// </summary>
    /// <remarks>
    /// This is the inverse of marker <see
    /// cref="WorldZero.Common.Interface.Entity.Marker.ISafeEntity"/>.
    /// </remarks>
    public interface IUnsafeEntity
    { }
}