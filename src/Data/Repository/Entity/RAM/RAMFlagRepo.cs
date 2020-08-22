using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="IFlagRepo">
    public class RAMFlagRepo
        : IRAMNamedEntityRepo<Flag>,
        IFlagRepo
    { }
}