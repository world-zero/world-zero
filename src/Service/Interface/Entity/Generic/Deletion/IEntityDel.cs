using System.Threading.Tasks;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityService{TEntity, TId, TBuiltIn}"/>
    /// <summary>
    /// This service class will handle deleting entities. For more, <see cref=
    /// "IEntityRepo.Delete()"/> and <see cref="IEntityRepo.Save()"/>.
    /// </summary>
    /// <remarks>
    /// None of these operations will will throw an exception if the entity
    /// does not exist.
    /// </remarks>
    public interface IEntityDel<TEntity, TId, TBuiltIn>
        : IEntityService<TEntity, TId, TBuiltIn>
        where TEntity : class, IEntity<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        void Delete(TId id);
        void Delete(TEntity e);
        Task DeleteAsync(TId id);
        Task DeleteAsync(TEntity e);
    }
}