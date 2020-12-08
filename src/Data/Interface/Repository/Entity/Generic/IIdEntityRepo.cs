using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.Primary.Generic
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `IIdEntity`.
    /// </summary>
    public interface IIdEntityRepo<TIdEntity>
        : IEntityRepo<TIdEntity, Id, int>
        where TIdEntity : ABCIdEntity
    { }
}