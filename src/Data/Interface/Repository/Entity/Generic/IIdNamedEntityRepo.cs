using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Data.Interface.Repository.Entity.Primary.Generic
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    /// <summary>
    /// This repo is responsible for implementations of `INamedEntity`.
    /// </summary>
    public interface IIdNamedEntityRepo<TIdNamedEntity>
        : IIdEntityRepo<TIdNamedEntity>
        where TIdNamedEntity : IIdEntity
    {
        TIdNamedEntity GetByName(Name name);
        Task<TIdNamedEntity> GetByNameAsync(Name name);
    }
}