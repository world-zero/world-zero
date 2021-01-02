using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.Generic
{
    /// <inheritdoc cref="IEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `IIdEntity`.
    /// </summary>
    public interface IIdEntityRepo<TIdEntity>
        : IEntityRepo<TIdEntity, Id, int>
        where TIdEntity : class, IIdEntity
    { }
}