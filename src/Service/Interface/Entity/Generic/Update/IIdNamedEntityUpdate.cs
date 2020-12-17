using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IEntityService{TEntity, TId, TBuiltIn}"/>
    public interface IIdNamedEntityUpdate<TEntity>
        : IEntityService<TEntity, Id, int>
        where TEntity : class, IIdNamedEntity
    {
        void AmendName(TEntity e, Name newName);
        void AmendName(Id entityId, Name newName);
        Task AmendNameAsync(TEntity e, Name newName);
        Task AmendNameAsync(Id entityId, Name newName);
    }
}