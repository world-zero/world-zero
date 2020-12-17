using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorldZero.Common.Interface.General.Generic
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
        Task<TClass> GetByIdAsync(TId id);

        /// <summary>
        /// Insert a new instance.
        /// </summary>
        /// <remarks>
        /// If the instance can detected internally as
        /// already registered, then this should use that to throw an
        /// exception if true.
        /// </remarks>
        void Insert(TClass obj);
        Task InsertAsync(TClass obj);

        /// <summary>
        /// Update an entity that is already inserted.
        /// </summary>
        /// <remarks>
        /// If the instance can detected internally as
        /// already registered, then this should use that to throw an
        /// exception if false. This extends to if the instance is just staged
        /// and not saved.
        /// </remarks>
        void Update(TClass obj);
        Task UpdateAsync(TClass obj);

        /// <summary>
        /// This will delete saved entities. If the entity is only staged, the
        /// entity is disregarded. If the entity is staged and saved, the
        /// entity will be re-staged to be deleted.
        /// </summary>
        void Delete(TId id);
        Task DeleteAsync(TId id);

        /// <summary>
        /// Save the stored instances and initiallize any IDs, if needed.
        /// </summary>
        /// <remarks>
        /// This should use Discard() in case of save failure, and no artifacts
        /// should exist. This will also perform deep copy saves.
        /// <br />
        /// This will not crash when something that does not exist is deleted.
        /// </remarks>
        void Save();
        Task SaveAsync();

        /// <summary>
        /// Reset the staged data to the default, empty staged state.
        /// </summary>
        void Discard();
        Task DiscardAsync();

        /// <summary>
        /// This will empty the stored data.
        /// </summary>
        void Clean();
        Task CleanAsync();
    }
}