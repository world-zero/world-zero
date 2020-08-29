using System;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Service.Interface
{
    /// <summary>
    /// This is a generic interface for entity creation service classes.
    /// </summary>
    /// <typeparam name="Entity">
    /// The IEntity implementation that this class abstracts.
    /// </typeparam>
    /// <typeparam name="IdType">
    /// This is the `ISingleValueObject` implementation that `Entity` uses as
    /// an ID.
    /// </typeparam>
    /// <typeparam name="SVOType">
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
    public abstract class IEntityRegistration<Entity, IdType, SVOType>
        where Entity : IEntity<IdType, SVOType>
        where IdType : ISingleValueObject<SVOType>
    {
        protected readonly IEntityRepo<Entity, IdType, SVOType> _repo;

        protected IEntityRegistration(IEntityRepo<Entity, IdType, SVOType> repo)
        {
            if (repo == null)
                throw new ArgumentNullException("repo");
            this._repo = repo;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual Entity Register(Entity e)
        {
            this.AssertNotNull(e);
            this._repo.Insert(e);
            this._repo.Save();
            return e;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual Entity RegisterAsync(Entity e)
        {
            // TODO: I have this issue logged.
            throw new NotImplementedException("This method is future work.");
        }

        protected void AssertNotNull(Entity e)
        {
            if (e == null)
                throw new ArgumentNullException();
        }
    }
}