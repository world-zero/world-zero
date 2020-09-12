using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IVoteRepo">
    public class RAMVoteRepo
        : IRAMIdIdRepo<Vote>,
          IVoteRepo
    { }
}