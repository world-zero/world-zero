using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IPraxisTagRepo"/>
    public class RAMPraxisTagRepo
        : IRAMEntityRelationRepo
          <
            PraxisTag,
            Id,
            int,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
          >,
          IPraxisTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new PraxisTag(new Id(3), new Name("d"));
            return a.GetUniqueRules().Count;
        }
    }
}