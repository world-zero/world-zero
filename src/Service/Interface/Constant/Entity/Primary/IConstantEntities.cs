using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Constant.Entity.Primary
{
    /// <summary>
    /// This interface denotes that the family of class that each contains the
    /// needed entities of that entity type for the system to work properly as
    /// static properties.
    /// </summary>
    /// <remarks>
    /// To have access to methods, instantiation is necessary. That said, most
    /// of these methods (namely the transaction-related ones) are not
    /// necessary and can be pretty safely ignored unless specifically needed.
    /// <br />
    /// These entities (should) also exist within the corresponding repo, but
    /// in an effort to not have a thousand costly queries, like
    /// IAbility.GetReiterator() or IStatus.GetActive(), this just aggregates
    /// those systemically significant entities into a class.
    /// <br />
    /// You may think that this is slightly unideal, and you would be correct.
    /// Since the various I{Entity}Service interfaces contain the concrete
    /// implementation for the corresponding I{Entity}, these methods are
    /// carried along from that interface to here. I could just have an
    /// interface that contains the concrete implementation and nothing else,
    /// but that just feels a bit weird, and having the concrete
    /// implementations be a part of a service just feels better, evn if it's a
    /// little weird as well.
    /// <br />
    /// Currently, the constant's registration services will ensure that it
    /// exists via `EnsureEntitiesExist`. A possible improvement would be to
    /// get ride of that feature and move it into <see cref="CzarConsole"/>
    /// during a first-time set up.
    /// </remarks>
    public abstract class IConstantEntities<TEntityRepo, TEntity, TId,TBuiltIn>
        where TEntityRepo : class, IEntityRepo<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        protected readonly TEntityRepo _repo;

        public IConstantEntities(TEntityRepo repo)
        {
            this.AssertNotNull(repo, "repo");
            this._repo = repo;
        }

        protected void EnsureExists(TEntity e)
        {
            this.AssertNotNull(e, "e");
            try
            {
                this._repo.Insert(e);
                this._repo.Save();
            }
            catch (ArgumentException) { }
        }

        public abstract IEnumerable<TEntity> GetEntities();

        public void EnsureEntitiesExist()
        {
            foreach (TEntity e in this.GetEntities())
                this.EnsureExists(e);
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