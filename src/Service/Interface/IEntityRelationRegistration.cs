using System;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Service.Interface
{
    /// <summary>
    /// This is a generic interface for entity relation creation service
    /// classes.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The IEntityRelation implementation that this class abstracts.
    /// </typeparam>
    /// <typeparam name="TLeftId">
    /// This is the `ISingleValueObject` implementation that `TEntity` uses as
    /// a left ID.
    /// </typeparam>
    /// <typeparam name="TLeftBuiltIn">
    /// This is the built-in type behind `TLeftId`.
    /// </typeparam>
    /// <typeparam name="TRightId">
    /// This is the `ISingleValueObject` implementation that `TEntity` uses as
    /// a right ID.
    /// </typeparam>
    /// <typeparam name="TRightBuiltIn">
    /// This is the built-in type behind `TRightId`.
    /// </typeparam>
    /// <remarks>
    /// For ease of use, it is recommended to have a property similar to the
    /// below to easily and consistently cast up in children. This example is
    /// taken from CreateEra.
    /// <code>
    /// protected IEraRepo _eraRepo { get { return (IEraRepo) this._repo; } }
    /// </code>
    /// </remarks>
    public abstract class IEntityRelationRegistration
        <TEntity, TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TEntity : IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        protected readonly IEntityRelationRepo
        <TEntity, TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> _repo;

        protected IEntityRelationRegistration(IEntityRelationRepo
            <TEntity, TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> repo)
        {
            if (repo == null)
                throw new ArgumentNullException("repo");
            this._repo = repo;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo.
        /// </summary>
        public virtual TEntity Register(TEntity e)
        {
            this.AssertNotNull(e);
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

        protected void AssertNotNull(TEntity e)
        {
            if (e == null)
                throw new ArgumentNullException();
        }
    }
}