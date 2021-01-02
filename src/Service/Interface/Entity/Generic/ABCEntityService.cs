using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic
{
    /// <inheritdoc cref="IEntityService{TEntity, TId, TBuiltIn}"/>
    public abstract class ABCEntityService<TEntity, TId, TBuiltIn>
        : IEntityService<TEntity, TId, TBuiltIn>
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
            this._repo.Transaction<TOperand>(operation, operand, serialize);
        }

        public async Task TransactionAsync<TOperand>(
            Action<TOperand> operation,
            TOperand operand,
            bool serialize=false
        )
        {
            this.AssertNotNull(operation, "operation");
            this.AssertNotNull(operand, "operand");
            await this._repo
                .TransactionAsync<TOperand>(operation, operand, serialize);
        }

        public void Transaction(
            Action operation,
            bool serialize=false
        )
        {
            this.AssertNotNull(operation, "operation");
            this._repo.Transaction(operation, serialize);
        }

        public async Task TransactionAsync(
            Action operation,
            bool serialize=false
        )
        {
            this.AssertNotNull(operation, "operation");
            await this._repo.TransactionAsync(operation, serialize);
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
    }
}