using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="ITagRepo">
    public class RAMTagRepo
        : IRAMNamedEntityRepo<Tag>,
        ITagRepo
    { }
}