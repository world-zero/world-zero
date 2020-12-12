using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityDel{TEntity, TId, TBuiltIn}"/>
    /// <summary>
    /// This service class will handle deleting entities. For more, <see cref=
    /// "IEntityRepo.Delete()"/>, <see cref="IEntityRepo.Save()"/>, and <see
    /// cref="IIdStatusedEntity"/>.
    /// </summary>
    public interface IIdStatusedEntityDel<TEntity>
        : IEntityDel<TEntity, Id, int>
        where TEntity : class, IIdStatusedEntity
    {
        void DeleteByStatus(IStatus s);
        void DeleteByStatus(Name statusId);
        Task DeleteByStatusAsync(IStatus s);
        Task DeleteByStatusAsync(Name statusId);
    }
}