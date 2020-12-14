using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityUnset{TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn}"/>
    public abstract class ABCEntityUnset
    <TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn>
        : ABCEntityDel<TEntity, TId, TBuiltIn>,
        IEntityUnset<TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>, IOptionalEntity
        where TId : ISingleValueObject<TBuiltIn>
        where TTEntity : class, IEntity<TTId, TTBuiltIn>, IEntityHasOptional
        where TTId : ISingleValueObject<TTBuiltIn>
    {
        protected readonly IEntityRepo<TTEntity, TTId, TTBuiltIn> _otherRepo;

        public ABCEntityUnset(
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

            IEnumerable<Character> chars = null;
            try
            { chars = this._charRepo.GetByLocationId(locationId); }
            catch (ArgumentException)
            { }

            if (chars != null)
            {
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