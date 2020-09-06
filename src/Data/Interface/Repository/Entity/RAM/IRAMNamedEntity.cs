using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Repository.Entity.RAM
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    public abstract class IRAMNamedEntityRepo<TEntity>
        : IRAMEntityRepo<TEntity, Name, string>,
          INamedEntityRepo<TEntity>
        where TEntity : INamedEntity
    { }
}