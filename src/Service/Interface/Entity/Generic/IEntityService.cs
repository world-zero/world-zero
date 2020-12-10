using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity
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
    public abstract class IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : IUnsafeEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        protected readonly IEntityRepo<TEntity, TId, TBuiltIn> _repo;

        public IEntityService(IEntityRepo<TEntity, TId, TBuiltIn> repo)
        {
            this.AssertNotNull(repo, "repo");
            this._repo = repo;
        }

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
        public void Transaction<TOperand>(
            Action<TOperand> operation,
            TOperand operand,
            bool serialize=false
        )
        {
            this.AssertNotNull(operation, "operation");
            this.AssertNotNull(operand, "operand");
            this.BeginTransaction(serialize);
            try
            { operation(operand); }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("The operation failed.", e);
            }
            catch (InvalidOperationException e)
            {
                this.DiscardTransaction();
                throw new InvalidOperationException("A bug has been found, discarding transaction.", e);
            }
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not complete the transaction.", e); }
            catch (InvalidOperationException e)
            { throw new InvalidOperationException("A bug has been found, discarding transaction.", e); }
        }

        /// <summary>
        /// Begin a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.BeginTransaction(bool)"/>
        /// </remarks>
        public void BeginTransaction(bool serialize=false)
        {
            this._repo.BeginTransaction(serialize);
        }

        /// <summary>
        /// Begin a transaction asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.BeginTransactionAsync(bool)"/>
        /// </remarks>
        public async Task BeginTransactionAsync(bool serialize=false)
        {
            await this._repo.BeginTransactionAsync(serialize);
        }

        /// <summary>
        /// End a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.EndTransaction()"/>
        /// </remarks>
        public void EndTransaction()
        {
            this._repo.EndTransaction();
        }

        /// <summary>
        /// End a transaction asynchronously. /// </summary> /// <remarks>
        /// For more, <see cref="IEntityRepo.EndTransactionAsync()"/>
        /// </remarks>
        public async Task EndTransactionAsync()
        {
            await this._repo.EndTransactionAsync();
        }

        /// <summary>
        /// Discard a transaction.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.DiscardTransaction()"/>
        /// </remarks>
        public void DiscardTransaction()
        {
            this._repo.DiscardTransaction();
        }

        /// <summary>
        /// Discard a transaction asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.DiscardTransactionAsync()"/>
        /// </remarks>
        public async Task DiscardTransactionAsync()
        {
            await this._repo.DiscardTransactionAsync();
        }

        /// <summary>
        /// Check if a transaction is active.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.IsTransactionActive()"/>
        /// </remarks>
        public bool IsTransactionActive()
        {
            return this._repo.IsTransactionActive();
        }

        /// <summary>
        /// Check if a transaction is active, asynchronously.
        /// </summary>
        /// <remarks>
        /// For more, <see cref="IEntityRepo.IsTransactionActiveAsync()"/>
        /// </remarks>
        public async Task<bool> IsTransactionActiveAsync()
        {
            return await this._repo.IsTransactionActiveAsync();
        }

        protected void AssertNotNull(object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }

        protected void EnsureExists(TEntity e)
        {
            try
            {
                this._repo.Insert(e);
                this._repo.Save();
            }
            catch (ArgumentException) { }
        }

        protected async Task EnsureExistsAsync(TEntity e)
        {
            this.AssertNotNull(e, "e");
            await Task.Run(() => this.EnsureExists(e));
        }
    }
}