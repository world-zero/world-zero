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
        where TEntity : IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        protected readonly IEntityRepo<TEntity, TId, TBuiltIn> _repo;

        public IEntityService(IEntityRepo<TEntity, TId, TBuiltIn> repo)
        {
            this.AssertNotNull(repo, "repo");
            this._repo = repo;
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