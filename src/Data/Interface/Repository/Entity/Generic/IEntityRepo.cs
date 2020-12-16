using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.Generic
{
    /// <summary>
    /// This is a generic repository for entities that includes CRUD methods.
    /// </summary>
    /// <remarks>
    /// On Save(), entities with an int ID will have that value be set.
    /// </remarks>
    public interface IEntityRepo<TEntity, TId, TIdBuiltIn>
        : IGenericRepo<TEntity, TId>
        where TEntity : class, IEntity<TId, TIdBuiltIn>
        where TId : ISingleValueObject<TIdBuiltIn>
    {
        /// <summary>
        /// This will remove all stored classes.
        /// </summary>
        void CleanAll();
        Task CleanAllAsync();

        /// <summary>
        /// This will, in order:
        /// <br />
        /// - Verify that the supplied arg is not null.
        /// <br />
        /// - Begin a transaction (serialization depends on the corresponding
        /// argument).
        /// <br />
        /// - Call the supplied `Action` being given `operand`; if the `Action`
        /// throws an `ArgumentException`, this method will
        /// `DiscardTransaction()` and throw a tracing `ArgumentException`.
        /// <br />
        /// - Attempt to `EndTransaction()`; if this throws an
        /// `ArgumentException`, then this will throw a tracing
        /// `ArgumentException`.
        /// <br />
        /// If an `InvalidOperationException` is thrown during the operation or
        /// `EndTransaction`, then this will discard the transaction and trace
        /// that exception.
        /// </summary>
        /// <param name="operation">
        /// The function to perform during the transaction with operand.
        /// </param>
        /// <param name="operand">
        /// The argument to pass to operation during the transaction.
        /// </param>
        /// <param name="serialize">
        /// This bool is supplied to `BeginTransaction(serialize)`.
        /// </param>
        /// <typeparam name="TOperand">
        /// The type of the argument supplied to operation.
        /// </typeparam>
        void Transaction<TOperand>(
            Action<TOperand> operation,
            TOperand operand,
            bool serialize=false
        );

        /// <summary>
        /// Perform the transaction asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="WorldZero.Data.Interface.Repository.Entity.Generic.IEntityRepo{TEntity, TId, TIdBuiltIn}.Transaction{TOperand}(Action{TOperand}, TOperand, bool)"/>
        /// </remarks>
        Task TransactionAsync<TOperand>(
            Action<TOperand> operation,
            TOperand operand,
            bool serialize=false
        );

        /// <summary>
        /// This will create a transaction between all repositories, allowing
        /// for nested transactions as described by the MS SQL Server docs.
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
        void BeginTransaction(bool serialize=false);
        Task BeginTransactionAsync(bool serialize=false);

        /// <summary>
        /// This will end a transaction and save. The end of a transaction will
        /// save atomically, even between different repository instances.
        /// </summary>
        /// <remarks>
        /// A database repository with different connections will not be able
        /// to save changes between the connections.
        /// <br />
        /// Per MS SQL Server docs, this will not end the entire transaction or
        /// commit the transaction iff it is nested within another transaction.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// This will be thrown iff an error occurs during `Save()`.
        /// </exception>
        void EndTransaction();
        Task EndTransactionAsync();

        /// <summary>
        /// Discard all of the changes made, reverting to the state just before
        /// calling this method; no exception will be thrown when called
        /// without an active transaction.
        /// </summary>
        /// <remarks>
        /// This will include reverting back to the saved and staged states
        /// that existed just before the transaction was started.
        /// <br />
        /// This will discard the entire transaction, even if the call occurs
        /// from within a nested transaction. This is done to mimic the MS SQL
        /// Server documentation surrounding ROLLBACK.
        /// </remarks>
        void DiscardTransaction();
        Task DiscardTransactionAsync();

        /// <summary>
        /// This return true iff a transaction is actve.
        /// </summary>
        bool IsTransactionActive();
        Task<bool> IsTransactionActiveAsync();
    }
}