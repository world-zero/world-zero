using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Generic
{
    /// <inheritdoc cref="IEntityService{TEntity, TId, TBuiltIn}"/>
    public abstract class ABCEntityService<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        protected readonly IEntityRepo<TEntity, TId, TBuiltIn> _repo;

        public ABCEntityService(IEntityRepo<TEntity, TId, TBuiltIn> repo)
        {
            this.AssertNotNull(repo, "repo");
            this._repo = repo;
        }

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

        public void BeginTransaction(bool serialize=false)
        {
            this._repo.BeginTransaction(serialize);
        }

        public async Task BeginTransactionAsync(bool serialize=false)
        {
            await this._repo.BeginTransactionAsync(serialize);
        }

        public void EndTransaction()
        {
            this._repo.EndTransaction();
        }

        public async Task EndTransactionAsync()
        {
            await this._repo.EndTransactionAsync();
        }

        public void DiscardTransaction()
        {
            this._repo.DiscardTransaction();
        }

        public async Task DiscardTransactionAsync()
        {
            await this._repo.DiscardTransactionAsync();
        }

        public bool IsTransactionActive()
        {
            return this._repo.IsTransactionActive();
        }

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