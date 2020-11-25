using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="INamedEntityRepo"/>
    public interface IFlagRepo
        : INamedEntityRepo<Flag>
    { }
}