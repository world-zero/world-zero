using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;
using System.Collections.Generic;
using System;

namespace WorldZero.Data.Interface.Repository.Entity.RAM
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// As the name suggests, this repo holds the entities in memory, with
    /// absolutely no persistence. The supplied and returned enties are deep
    /// copies to those that are saved and staged. This is not going to enforce
    /// that foreign keys are set (outside of relational entities), or perform
    /// any other special database features or functionality.
    /// In an effort to be similar to a database-connecting repo, this is only
    /// going to throw exceptions about conflicts between staged and saved
    /// entities on Save.
    /// </summary>
    public abstract class IRAMEntityRepo<TEntity, TId, TSingleValObj>
        : IEntityRepo<TEntity, TId, TSingleValObj>
        where TEntity : IEntity<TId, TSingleValObj>
        where TId : ISingleValueObject<TSingleValObj>
    {
        // These will use use the ID of an entity to key that entity. For
        // staged changes, a null entity reference indicates that the entity
        // of that ID should be deleted. As for inserts and updates, there is
        // no real distinction, except inserts will utilize a method that takes
        // the new entity, and return an ID to use as the key; this is
        // necessary in cases of repo-generated IDs. By default, this method
        // will simply return the ID of the supplied entity.
        protected Dictionary<TId, TEntity> _saved;
        protected Dictionary<TId, TEntity> _staged;
        protected virtual TId GenerateId(TEntity entity)
        {
            return entity.Id;
        }

        public IRAMEntityRepo()
        {
            this._saved  = new Dictionary<TId, TEntity>();
            this._staged = new Dictionary<TId, TEntity>();
        }

        /// <summary>
        /// Get all of the saved entities as an enumerable.
        /// </summary>
        /// <returns>The saved entities enumerable.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            foreach(KeyValuePair<TId, TEntity> pair in this._saved)
            {
                TId id = pair.Key;
                TEntity entity = pair.Value;
                if ( (entity.Id != id) || (!entity.IsIdSet()) )
                    throw new InvalidOperationException("A saved entity without an ID has been discovered.");
                yield return (TEntity) entity.DeepCopy();
            }
        }

        /// <remarks>
        /// This will only search the saved entities.
        /// </remarks>
        public virtual TEntity GetById(TId id)
        {
            if (!this._saved.ContainsKey(id))
                throw new ArgumentException("You cannot get an entity with an ID does not exist.");
            return (TEntity) this._saved[id].DeepCopy();
        }

        /// <remarks>
        /// This does NOT do anything to verify that the entity is new and not
        /// already stored. GenerateId would be a perfect place to do this.
        /// </remarks>
        public virtual void Insert(TEntity entity)
        {
            this._staged[this.GenerateId(entity)] = entity;
        }

        /// <summary>
        /// This will update saved entities and update staged entities if
        /// already staged with a set ID. This will not let unsaved entities
        /// that are staged be updated.
        /// </summary>
        public virtual void Update(TEntity entity)
        {
            if (!entity.IsIdSet())
                throw new InvalidOperationException("An entity cannot be updated if the ID is unset, as stored entities will have a set ID on save.");
            this._staged[entity.Id] = entity;
        }

        /// <summary>
        /// This will delete saved entities. If the entity is only staged, the
        /// entity is disregarded (assuming it has an ID type that must be set
        /// on initialization, like `Name`s). If the entity is staged and
        /// saved, the entity will be staged to be deleted.
        /// </summary>
        public virtual void Delete(TId id)
        {
            this._staged[id] = null;
        }

        /// <summary>
        /// This will save the staged entities. In the case of repo-generated
        /// IDs, the reference will have its ID set then.
        /// </summary>
        public virtual void Save()
        {
            foreach (KeyValuePair<TId, TEntity> pair in this._staged)
            {
                TId id = pair.Key;
                TEntity entity = pair.Value;

                if (entity == null)
                    this.CommitDelete(id);
                else
                {
                    if (!entity.IsIdSet())
                        entity.Id = id;
                    this.CommitChange(id, entity);
                }

                this._staged.Remove(id);
            }

            var c = this._staged.Count;
            if (c != 0)
                throw new InvalidOperationException($"After a Save, there should be no staged entities, but there are {c} staged entities remaining.");
        }

        /// <summary>
        /// Delete the entity with the matching ID. If there is no entity to
        /// delete, then it must be an erroneous deletion (trying to delete
        /// something unsaved), so this will return null.
        /// </summary>
        protected virtual TEntity CommitDelete(TId id)
        {
            if (this._saved.ContainsKey(id))
            {
                var old = this._saved[id];
                this._saved.Remove(id);
                return old;
            }
            return null;
        }

        protected virtual TEntity CommitChange(TId id, TEntity entity)
        {
            TEntity old;
            if (this._saved.ContainsKey(id))
                old = this._saved[id];
            else
                old = null;
            this._saved[id] = (TEntity) entity.DeepCopy();
            return old;
        }
    }
}