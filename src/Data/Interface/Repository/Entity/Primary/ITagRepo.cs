using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="INamedEntityRepo"/>
    public interface ITagRepo
        : INamedEntityRepo<ITag>
    { }
}