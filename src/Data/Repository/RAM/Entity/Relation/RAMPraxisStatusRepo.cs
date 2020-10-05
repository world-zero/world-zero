using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IPraxisStatusRepo"/>
    public class RAMPraxisStatusRepo
        : IRAMEntityRelationRepo
          <
            PraxisStatus,
            Id,
            int,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
          >,
          IPraxisStatusRepo
    {
        protected override int GetRuleCount()
        {
            var a = new PraxisStatus(new Id(3), new Name("sdf"));
            return a.GetUniqueRules().Count;
        }
    }
}