using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityDel{TEntity, TId, TBuiltIn}"/>
    /// <summary>
    /// In addition to handling the deletion of entities, this will provide the
    /// ability to unset all references to the entity - this involves unsetting
    /// the various references to the entity. For more, see
    /// <see cref="WorldZero.Common.Interface.Entity.Unspecified.Primary.IOptionalEntity"/>.
    /// Naturally, deleting an entity with this class will set all references
    /// to it to null.
    /// </summary>
    /// <remarks>
    /// An example of this would
    /// be unsetting Faction X, where the `Unset` method will change all
    /// characters with that faction to no longer have that faction. Crucially,
    /// these entities must be optional, like how characters do not need a
    /// faction.
    /// <br />
    /// Testing for this class is performed via testing `LocationUnset` as it
    /// does not deviate.
    /// <br />
    /// Since this requires adjusting at least one other repo, this has generic
    /// types to easily supply a second repo. As usual, it is recommended to
    /// create a wrapper property for `_otherRepo` with a better name in
    /// concrete classes.
    /// <br />
    /// This code is a little smelly since it's redundant logic, but it's an
    /// pretty infrequently occurring case, so bite me. The more you think
    /// about this case, the more you will see that it's a lot of effort to
    /// solve a mild smell. For the logic to copy/paste, see the excerpt under
    /// the abstract Unset(TId) signature. All of the cases are as follows:
    /// <br />- Character.LocationId
    /// <br />- Character.FactionId
    /// <br />- Faction.AbilityId
    /// <br />- Praxis.MetaTask
    /// </remarks>
    public interface IEntityUnset
    <TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn>
        : IEntityDel<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>, IOptionalEntity
        where TId : ABCSingleValueObject<TBuiltIn>
        where TTEntity : class, IEntity<TTId, TTBuiltIn>, IEntityHasOptional
        where TTId : ABCSingleValueObject<TTBuiltIn>
    {
        void Unset(TId id);
        void Unset(TEntity e);
        Task UnsetAsync(TId id);
        Task UnsetAsync(TEntity e);
    }
}