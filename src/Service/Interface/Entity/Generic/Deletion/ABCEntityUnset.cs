using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityUnset{TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn}"/>
    public abstract class ABCEntityUnset
    <TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn>
        : ABCEntityDel<TEntity, TId, TBuiltIn>,
        IEntityUnset<TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>, IOptionalEntity
        where TId : ABCSingleValueObject<TBuiltIn>
        where TTEntity : class, IEntity<TTId, TTBuiltIn>, IEntityHasOptional
        where TTId : ABCSingleValueObject<TTBuiltIn>
    {
        protected readonly IEntityRepo<TTEntity, TTId, TTBuiltIn> _otherRepo;
        protected readonly IEntityUpdate<TTEntity, TTId, TTBuiltIn> _otherUpdate;

        public ABCEntityUnset(
            IEntityRepo<TEntity, TId, TBuiltIn> repo,
            IEntityRepo<TTEntity, TTId, TTBuiltIn> otherRepo,
            IEntityUpdate<TTEntity, TTId, TTBuiltIn> otherUpdate
        )
            : base(repo)
        {
            this.AssertNotNull(otherRepo, "otherRepo");
            this.AssertNotNull(otherUpdate, "otherUpdate");
            this._otherRepo = otherRepo;
            this._otherUpdate = otherUpdate;
        }

        // TODO: Through some hideous helper with several delegates, this might
        // be refactorable.
        public abstract void Unset(TId id);
        /* The following excerpt is taken from LocationUnset.cs
        public override void Unset(Id locationId)
        {
            this.AssertNotNull(locationId, "locationId");
            this.BeginTransaction();

            IEnumerable<Character> chars = null;
            try
            { chars = this._charRepo.GetByLocationId(locationId); }
            catch (ArgumentException)
            { }

            if (chars != null)
            {
                Id newId = null;
                foreach (Character c in chars)
                    this._otherUpdate.AmendLocation(c, newId);
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
            void f(TId id0)
            {
                base.Delete(id);
                this.Unset(id);
            }

            this.Transaction<TId>(f, id);
        }
    }
}