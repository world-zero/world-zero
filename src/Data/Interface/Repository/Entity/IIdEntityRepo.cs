using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `IIdEntity`.
    /// </summary>
    public interface IIdEntityRepo<TIdEntity>
        : IEntityRepo<TIdEntity, Id, int>
        where TIdEntity : IIdEntity
    { }
}