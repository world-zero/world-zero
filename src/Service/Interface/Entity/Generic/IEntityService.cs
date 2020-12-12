using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Service.Interface.Entity.Generic
{
    /// <summary>
    /// This is a generic interface for entity service classes.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The IEntity implementation that this class abstracts.
    /// </typeparam>
    /// <typeparam name="TId">
    /// This is the `ISingleValueObject` implementation that `Entity` uses as
    /// an ID.
    /// </typeparam>
    /// <typeparam name="TBuiltIn">
    /// This is the built-in type behind `IdType`.
    /// </typeparam>
    /// <remarks>
    /// For ease of use, it is recommended to have a property similar to the
    /// below to easily and consistently cast up in children. This example is
    /// taken from CreateEra.
    /// <code>
    /// protected IEraRepo _eraRepo { get { return (IEraRepo) this._repo; } }
    /// </code>
    /// </remarks>
    public interface IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
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
        /// Begin a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.BeginTransaction(bool)"/>
        /// </remarks>
        void BeginTransaction(bool serialize=false);

        /// <summary>
        /// Begin a transaction asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.BeginTransactionAsync(bool)"/>
        /// </remarks>
        Task BeginTransactionAsync(bool serialize=false);

        /// <summary>
        /// End a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.EndTransaction()"/>
        /// </remarks>
        void EndTransaction();

        /// <summary>
        /// End a transaction asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.EndTransactionAsync()"/>
        /// </remarks>
        Task EndTransactionAsync();

        /// <summary>
        /// Discard a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.DiscardTransaction()"/>
        /// </remarks>
        void DiscardTransaction();

        /// <summary>
        /// Discard a transaction asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.DiscardTransactionAsync()"/>
        /// </remarks>
        Task DiscardTransactionAsync();

        /// <summary>
        /// Check if a transaction is active.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.IsTransactionActive()"/>
        /// </remarks>
        bool IsTransactionActive();

        /// <summary>
        /// Check if a transaction is active, asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.IsTransactionActiveAsync()"/>
        /// </remarks>
        Task<bool> IsTransactionActiveAsync();
    }
}