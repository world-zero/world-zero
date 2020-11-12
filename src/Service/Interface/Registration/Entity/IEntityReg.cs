using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Service.Interface.Registration.Entity
{
    /// <summary>
    /// This is a generic interface for entity creation service classes.
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
    public abstract class IEntityReg<TEntity, TId, TBuiltIn>
        where TEntity : IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        protected readonly IEntityRepo<TEntity, TId, TBuiltIn> _repo;

        protected IEntityReg(IEntityRepo<TEntity, TId, TBuiltIn> repo)
        {
            this.AssertNotNull(repo, "repo");
            this._repo = repo;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual TEntity Register(TEntity e)
        {
            this.AssertNotNull(e, "e");
            try
            {
                this._repo.Insert(e);
                this._repo.Save();
            }
            catch (ArgumentException exc)
            { throw new ArgumentException("Failed to complete a register.", exc); }
            return e;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual async Task<TEntity> RegisterAsync(TEntity e)
        {
            return await Task.Run(() => this.Register(e));
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
    }
}