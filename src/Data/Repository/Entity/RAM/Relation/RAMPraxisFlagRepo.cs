using System.Threading.Tasks;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IPraxisFlagRepo"/>
    public class RAMPraxisFlagRepo
        : IRAMFlaggedEntityRepo
          <
            IPraxisFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          IPraxisFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafePraxisFlag(new Id(3), new Name("sdf"));
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