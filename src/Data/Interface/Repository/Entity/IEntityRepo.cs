using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <summary>
    /// This is a generic repository for entities that includes CRUD methods.
    /// </summary>
    /// <remarks>
    /// On Save(), entities with an int ID will have that value be set.
    /// </remarks>
    public interface IEntityRepo<TEntity, TId, TIdBuiltIn>
        : IGenericRepo<TEntity, TId>
        where TEntity : IEntity<TId, TIdBuiltIn>
        where TId : ISingleValueObject<TIdBuiltIn>
    {
        /// <summary>
        /// This will remove all static data for a concrete instance of
        /// IEntityRepo.
        /// </summary>
        void Clean();

        /// <summary>
        /// This will remove all static data for all concrete instances of
        /// IEntityRepo.
        /// </summary>
        void CleanAll();

        // /// <summary>
        // /// This will create a transaction between all repositories.
        // /// </summary>
        // /// <exception cref="ArgumentException">
        // /// This is thrown if there is already an active transaction.
        // /// </exception>
        // void BeginTransaction();

        // /// <summary>
        // /// This will end a transaction and save. The end of a transaction will
        // /// save atomically, even between different repository instances.
        // /// </summary>
        // /// <remarks>
        // /// A database repository with different connections will not be able
        // /// to save changes between the connections.
        // /// </remarks>
        // /// <exception cref="ArgumentException">
        // /// This is thrown if an error occurs during the transaction's save.
        // /// </exception>
        // void EndTransaction();

        // /// <summary>
        // /// Discard all of the changes made, reverting to the state just before
        // /// calling this method.
        // /// </summary>
        // /// <remarks>
        // /// This will include reverting back to the saved and staged states
        // /// that existed just before the transaction was started.
        // /// </remarks>
        // void DiscardTransaction();
    }
}