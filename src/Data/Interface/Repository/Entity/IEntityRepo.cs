using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <summary>
    /// This is a generic repository for entities that includes CRUD methods.
    /// </summary>
    /// <remarks>
    /// On Save(), entities with an int ID will have that value be set.
    /// </remarks>
    public interface IEntityRepo<TEntity, TId, TSingleValObj>
        : IGenericRepo<TEntity, TId>
        where TEntity : IEntity<TId, TSingleValObj>
        where TId : ISingleValueObject<TSingleValObj>
    { }
}