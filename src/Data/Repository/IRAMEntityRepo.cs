using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;
using WorldZero.Data.Interface.Entity;
using System.Collections.Generic;
using System;

/* TODO
    then make concrete "implementations" for IdEntity and NamedEntity;
    then move onto IdNamedEntity
*/

namespace WorldZero.Data.Repository
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// As the name suggests, this repo holds the entities in memory, with
    /// absolutely no persistence. The supplied and returned enties are SHALLOW
    /// COPIES to those that are saved and staged. This is acceptable because
    /// this is more of a dev tool than an actual product tool.
    /// </summary>
    public abstract class IRAMEntityRepo<Entity, IdType, SVOType>
        : IEntityRepo<Entity, IdType, SVOType>
        where Entity : IEntity<IdType, SVOType>
        where IdType : ISingleValueObject<SVOType>
    {
        // These will use use the ID of an entity to key that entity. For
        // staged changes, a null entity reference indicates that the entity
        // of that ID should be deleted. As for inserts and updates, there is
        // no real distinction, except inserts will utilize a method that takes
        // the new entity, and return an ID to use as the key; this is
        // necessary in cases of repo-generated IDs. By default, this method
        // will simply return the ID of the supplied entity.
        protected Dictionary<IdType, Entity> _saved;
        protected Dictionary<IdType, Entity> _staged;
        protected virtual IdType GenerateId(Entity entity)
        {
            return entity.Id;
        }

        public IRAMEntityRepo()
        {
            this._saved  = new Dictionary<IdType, Entity>();
            this._staged = new Dictionary<IdType, Entity>();
        }

        /// <summary>
        /// Get all of the saved entities as an enumerable.
        /// </summary>
        /// <returns>The saved entities enumerable.</returns>
        public IEnumerable<Entity> GetAll()
        {
            foreach(KeyValuePair<IdType, Entity> pair in this._saved)
            {
                IdType id = pair.Key;
                Entity entity = pair.Value;
                if ( (entity.Id != id) || (!entity.IsIdSet()) )
                    throw new InvalidOperationException("An internal consistency error has been discovered.");
                yield return entity;
            }
        }

        public Entity GetById(IdType id)
        {
            if (!this._saved.ContainsKey(id))
                throw new ArgumentException("You cannot get an entity with an ID does not exist.");
            return this._saved[id];
        }

        public void Insert(Entity entity)
        {
            this._staged[this.GenerateId(entity)] = entity;
        }

        public void Update(Entity entity)
        {
            if (!this._saved.ContainsKey(entity.Id))
                throw new ArgumentException("You cannot update an unstored entity.");
            if (!entity.IsIdSet())
                throw new InvalidOperationException("An entity cannot be updated if the ID is unset, as stored entities will have a set ID on save.");
            this._staged[entity.Id] = entity;
        }

        public void Delete(IdType id)
        {
            if (!this._saved.ContainsKey(id))
                throw new ArgumentException("You cannot delete an unstored entity.");
            this._staged[id] = null;
        }

        /// <summary>
        /// This will save the staged entities. In the case of repo-generated
        /// IDs, the reference will have its ID set then.
        /// </summary>
        public void Save()
        {
            foreach(KeyValuePair<IdType, Entity> pair in this._staged)
            {
                IdType id = pair.Key;
                Entity entity = pair.Value;

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
        }

        protected virtual void CommitDelete(IdType id)
        {
            if (!this._saved.ContainsKey(id))
                throw new InvalidOperationException("An internal consistency error has occurred: attempted to remove an ID that does not exist; this should have been disallowed during Delete().");
            this._saved.Remove(id);
        }

        protected virtual void CommitChange(IdType id, Entity entity)
        {
            this._saved[id] = entity;
        }
    }
}