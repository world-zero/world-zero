using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="INamedEntityRepo"/>
    public interface IStatusRepo
        : INamedEntityRepo<Status>
    { }
}