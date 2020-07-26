using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface;
using System.Collections.Generic;
using System;

namespace WorldZero.Data.Repository
{
    /*
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// As the name suggests, this repo holds the entities in memory, with
    /// absolutely no persistence.
    /// </summary>
    public abstract class RAMEntityRepo<T> : IEntityRepo<T> where T : IEntity
    {
        // Since we have no method to unstage, we know that the ID will
        // never need to be rolled back.
        private int _nextId;
        private Dictionary<Id, T> _saved;

        /// <summary>
        /// The key will be the ID of the new entity to
        /// store the order of new additions. Values that are null indicate
        /// that the entity should be deleted.
        /// </summary>
        private Dictionary<Id, T> _staged;

        public RAMEntityRepo()
        {
            this._nextId = 1;
            this._saved  = new Dictionary<Id, T>();
            this._staged = new Dictionary<Id, T>();
        }

        /// <summary>
        /// Get all of the saved entities as an enumerable.
        /// </summary>
        /// <returns>The saved entities enumerable.</returns>
        public IEnumerable<T> GetAll()
        {
            foreach(KeyValuePair<Id, T> pair in this._saved)
            {
                Id id = pair.Key;
                T entity = pair.Value;
                if (new Id(entity.Id) != id)
                    throw new InvalidOperationException("An internal consistency error has been discovered.");
                yield return entity;
            }
        }

        /// <remarks>
        /// This method expects to receive an integer.
        /// </remarks>
        public T GetById(object intId)
        {
            int i = (int) intId;
            if (i < 1)
                return null;
            
            Id d = new Id(i);
            if (this._saved.ContainsKey(d))
                return this._saved[d];
            else
                return null;
        }

        public void Insert(T entity)
        {
            this._staged.Add(new Id(this._nextId++), entity);
        }

        public void Update(T entity)
        {
            this._staged.Add(new Id(entity.Id), entity);
        }

        /// <remarks>
        /// This method expects to receive an integer.
        /// </remarks>
        public void Delete(object intId)
        {
            int i = (int) intId;
            Id d = new Id(i);
            if (!this._saved.ContainsKey(d))
                throw new ArgumentException("You cannot delete an ID of an entity does not exist.");
            this._staged.Add(d, null);
        }

        public void Save()
        {
            foreach(KeyValuePair<Id, T> pair in this._staged)
            {
                Id id = pair.Key;
                T entity = pair.Value;

                if (entity == null)
                    this.CommitDelete(id);
                else
                {
                    if (id == new Id(0))
                        entity.Id = id.Get;
                    this.CommitChange(id, entity);
                }
            }
        }

        protected virtual void CommitDelete(Id id)
        {
            if (this._saved.ContainsKey(id))
                this._saved.Remove(id);
            else
                throw new InvalidOperationException("An internal consistency error has occurred: attempted to remove an ID that does not exist; this should have been disallowed during Delete().");
        }

        protected virtual void CommitChange(Id id, T entity)
        {
            this._saved[id] = entity;
        }
    }
    */
}