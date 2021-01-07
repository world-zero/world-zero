using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

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
    /// Each non-generic entity service should have a protected concrete
    /// implementation of the corresponding entity. This will allow the various
    /// entity service classes to be able to edit the entities without exposing
    /// the setters or constructor to the outside world. This has two main
    /// purposes. First, this will force the application to use an entity's
    /// updating service class to change the data as the entity interfaces only
    /// have getters. Second, as an entity will not have a publicly available
    /// concrete implementation, the application will be forced to use an
    /// entity's service classes as factories, which ensures that no one is
    /// "updating" an entity by creating a very similar clone and sneaking that
    /// corrupted version past the updating service classes.
    /// <br />
    /// For ease of use, it is recommended to have a property similar to the
    /// below to easily and consistently cast up in children. This example is
    /// taken from CreateEra.
    /// <code>
    /// protected IEraRepo _eraRepo { get { return (IEraRepo) this._repo; } }
    /// </code>
    /// </remarks>
    public interface IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        /// <summary>
        /// Perform the supplied Action as a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="WorldZero.Data.Interface.Repository.Entity.Generic.IEntityRepo{TEntity, TId, TIdBuiltIn}.Transaction{TOperand}(Action{TOperand}, TOperand, bool)"/>
        /// </remarks>
        void Transaction<TOperand>(
            Action<TOperand> operation,
            TOperand operand,
            bool serialize=false
        );

        Task TransactionAsync<TOperand>(
            Action<TOperand> operation,
            TOperand operand,
            bool serialize=false
        );

        /// <summary>
        /// Perform the supplied Action as a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="WorldZero.Data.Interface.Repository.Entity.Generic.IEntityRepo{TEntity, TId, TIdBuiltIn}.Transaction(Action, bool)"/>
        /// </remarks>
        void Transaction(
            Action operation,
            bool serialize=false
        );

        Task TransactionAsync(
            Action operation,
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