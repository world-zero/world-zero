using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IVoteRepo"/>
    public class RAMVoteRepo
        : IRAMIdIdRepo<Vote>,
          IVoteRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Vote(new Id(2), new Id(2), new PointTotal(3));
            return a.GetUniqueRules().Count;
        }
    }
}