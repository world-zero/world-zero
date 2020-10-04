using System;
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
    /// <typeparam name="TSingleValObj">
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
    public abstract class IEntityReg<TEntity, TId, TSingleValObj>
        where TEntity : IEntity<TId, TSingleValObj>
        where TId : ISingleValueObject<TSingleValObj>
    {
        protected readonly IEntityRepo<TEntity, TId, TSingleValObj> _repo;

        protected IEntityReg(IEntityRepo<TEntity, TId, TSingleValObj> repo)
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
            this._repo.Insert(e);
            this._repo.Save();
            return e;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual TEntity RegisterAsync(TEntity e)
        {
            // TODO: I have this issue logged.
            throw new NotImplementedException("This method is future work.");
        }

        protected void AssertNotNull(object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }
    }
}