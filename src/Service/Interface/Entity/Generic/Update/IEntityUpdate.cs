using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <summary>
    /// This is a generic interface for entity updating service classes.
    /// These can update a
    /// specific property and/or perform a complex change, like migrating all
    /// active praxises to retired.
    /// </summary>
    /// <remarks>
    /// Naturally, you cannot edit the ID of an entity. Additionally, if the
    /// entity is a relational entity, this family of classes should not be
    /// responsible for changing what two entities a relation relates, or the
    /// count of that relation.
    /// <br />
    /// These will contain pretty comprehensive updating methods. Naturally,
    /// do not expose all of these in the API - pick and choose. Additionally,
    /// these methods will throw an exception iff the new value cannot be
    /// saved, regardless of whether or not the new value is different from
    /// what it was.
    /// <br />
    /// If an update is called and only supplied the ID of the entity to
    /// update, then the whole entity is pulled from the repository before the
    /// update occurs. This is done to ensure that the update is not going to
    /// be breaking some system logic. That said, not all updates need to
    /// perform this check, and a valid refactor would be incorporate
    /// update mechanisms for these isolated updates.
    /// <br />
    /// If an entity is supplied in place of an ID, then there will be no query
    /// performed. This heavily relies on the fact that repositories do not
    /// allow for unsaved entities to be updated.
    /// <br />
    /// Abstract Children not implement methods as we would need to cast the
    /// entity interface (which only has getters). We could get around this by
    /// having another generic type we could cast to that contains setters, but
    /// that implementation is super not public, and also that idea is very
    /// smelly.
    /// <br />
    /// This intentionally does not have any type of compiler-enforced
    /// mechanism for ensuring that all of an entity's properties are amendable
    /// via this tool.
    /// <br />
    /// Dev note: The non-generic entity update interfaces can extend the max
    /// row character count to 100.
    /// </remarks>
    /// <inheritdoc cref="IEntityService{TEntity, TId, TBuiltIn}"/>
    public interface IEntityUpdate<TEntity, TId, TBuiltIn>
        : IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    { }
}