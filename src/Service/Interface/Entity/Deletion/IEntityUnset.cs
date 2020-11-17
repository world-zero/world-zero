using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Interface.Entity.Deletion
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// In addition to handling the deletion of entities, this will provide the
    /// ability to unset all references to the entity - this involves unsetting
    /// the various references to the entity. For more, see
    /// <see cref="WorldZero.Common.Interface.Entity.IOptionalEntity"/>.
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
    public abstract class IEntityUnset
    <TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn>
        : IEntityDel<TEntity, TId, TBuiltIn>
        where TEntity : IEntity<TId, TBuiltIn>, IOptionalEntity
        where TId : ISingleValueObject<TBuiltIn>
        where TTEntity : IEntity<TTId, TTBuiltIn>, IEntityHasOptional
        where TTId : ISingleValueObject<TTBuiltIn>
    {
        protected readonly IEntityRepo<TTEntity, TTId, TTBuiltIn> _otherRepo;

        public IEntityUnset(
            IEntityRepo<TEntity, TId, TBuiltIn> repo,
            IEntityRepo<TTEntity, TTId, TTBuiltIn> otherRepo
        )
            : base(repo)
        {
            this.AssertNotNull(otherRepo, "otherRepo");
            this._otherRepo = otherRepo;
        }

        public abstract void Unset(TId id);
        /* The following excerpt is taken from LocationUnset.cs
        public override void Unset(Id locationId)
        {
            this.AssertNotNull(locationId, "locationId");
            this.BeginTransaction();

            IEnumerable<Character> chars;
            try
            { chars = this._charRepo.GetByLocationId(locationId); }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not complete the unset.", e);
            }

            foreach (Character c in chars)
            {
                c.LocationId = null;
                try
                { this._charRepo.Update(c); }
                catch (ArgumentException e)
                {
                    this.DiscardTransaction();
                    throw new ArgumentException("Could not complete the unset.", e);
                }
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
        */

        public void Unset(TEntity e)
        {
            AssertNotNull(e, "e");
            this.Unset(e.Id);
        }

        public virtual async Task UnsetAsync(TId id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.Unset(id));
        }

        public async Task UnsetAsync(TEntity e)
        {
            AssertNotNull(e, "e");
            await Task.Run(() => this.Unset(e.Id));
        }

        public override void Delete(TId id)
        {
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            try
            {
                base.Delete(id);
                this.Unset(id);
            }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not complete the delete, reverting.", e);
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
    }
}