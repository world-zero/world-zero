using System.Threading.Tasks;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IPraxisTagRepo"/>
    public class RAMPraxisTagRepo
        : IRAMTaggedEntityRepo
          <
            PraxisTag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          IPraxisTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new PraxisTag(new Id(3), new Name("d"));
            return a.GetUniqueRules().Count;
        }

        public void DeleteByPraxisId(Id praxisId)
        {
            this.DeleteByLeftId(praxisId);
        }

        public async Task DeleteByPraxisIdAsync(Id praxisId)
        {
            this.DeleteByPraxisId(praxisId);
        }
    }
}