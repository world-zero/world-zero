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

        /// <summary>
        /// This will create a transaction between all repositories.
        /// </summary>
        /// <param name="serialize">
        /// If true, the transaction should act like
        /// `SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;` was entered just
        /// before the transaction was begun. This will ensure that:
        /// <br />
        /// - Statements cannot read data that has been modified but not yet
        /// committed by other transactions.
        /// <br />
        /// - No other transactions can modify data that has been read by the
        /// current transaction until the current transaction completes.
        /// <br />
        /// - Other transactions cannot insert new rows with key values that
        /// would fall in the range of keys read by any statements in the
        /// current transaction until the current transaction completes.
        /// </param>
        /// <exception cref="ArgumentException">
        /// This is thrown if there is already an active transaction.
        /// </exception>
        void BeginTransaction(bool serialize=false);

        /// <summary>
        /// This will end a transaction and save. The end of a transaction will
        /// save atomically, even between different repository instances.
        /// </summary>
        /// <remarks>
        /// A database repository with different connections will not be able
        /// to save changes between the connections.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// This is thrown if an error occurs during the transaction's save.
        /// </exception>
        void EndTransaction();

        /// <summary>
        /// Discard all of the changes made, reverting to the state just before
        /// calling this method; no exception will be thrown when called
        /// without an active transaction.
        /// </summary>
        /// <remarks>
        /// This will include reverting back to the saved and staged states
        /// that existed just before the transaction was started.
        /// </remarks>
        void DiscardTransaction();

        /// <summary>
        /// This return true iff a transaction is actve.
        /// </summary>
        bool IsTransactionActive();
    }
}