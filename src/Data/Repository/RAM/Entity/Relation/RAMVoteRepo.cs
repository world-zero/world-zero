using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IVoteRepo"/>
    public class RAMVoteRepo
        : IRAMEntityRelationRepo
          <
            Vote,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >,
          IVoteRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Vote(new Id(2), new Id(2), new PointTotal(3));
            return a.GetUniqueRules().Count;
        }
    }
}