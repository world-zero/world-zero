using WorldZero.Common.Interface;
using WorldZero.Data.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace WorldZero.Data.Repository
{
    /*
    /// <inheritdoc cref="IEntityRepo"/>
    public class EFCoreRepo<T> : IEntityRepo<T> where T : IEntity
    {
        private W0Context _context;
        private DbSet<T> _table;

        public EFCoreRepo(W0Context context)
        {
            if (context == null)
                throw new ArgumentException("The DbContext cannot be null.");
            this._context = context;
            this._table = this._context.Set<T>();

            try { this._table.ToString(); }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("An IEntity that is not in the database was supplied.");
            }
        }

        public IEnumerable<T> GetAll()
        {
            return this._table.ToList();
        }

        public T GetById(object id)
        {
            return this._table.Find(id);
        }

        public void Insert(T obj)
        {
            this._table.Add(obj);
        }

        public void Update(T obj)
        {
            this._table.Attach(obj);
            this._context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = this._table.Find(id);
            this._table.Remove(existing);
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }
    */
}