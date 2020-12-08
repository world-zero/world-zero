using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    public abstract class IRAMNamedEntityRepo<TEntity>
        : IRAMEntityRepo<TEntity, Name, string>,
          INamedEntityRepo<TEntity>
        where TEntity : ABCNamedEntity
    { }
}