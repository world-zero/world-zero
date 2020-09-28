using System.Collections.Generic;

namespace WorldZero.Common.Interface
{
    public interface IGenericRepo<TClass, TId>
        where TClass : class
    {
        IEnumerable<TClass> GetAll();
        /// <summary>
        /// Get a saved item by its ID. If there is no corresponding ID, then
        /// an exception is thrown.
        /// </summary>
        TClass GetById(TId id);
        void Delete(TId id);
        void Insert(TClass obj);
        void Update(TClass obj);
        /// <summary>
        /// Save the stored instances and initiallize any IDs, if needed.
        /// </summary>
        /// <remarks>
        /// This should use Discard() in case of save failure, and no artifacts
        /// should exist. This will also perform deep copy saves.
        /// </remarks>
        void Save();
        void Discard();
    }
}