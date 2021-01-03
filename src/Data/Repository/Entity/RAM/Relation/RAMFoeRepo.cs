using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IFoeRepo"/>
    public class RAMFoeRepo
        : IRAMEntitySelfRelationRepo
          <
            Foe,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >,
          IFoeRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Foe(new Id(3), new Id(2));
            return a.GetUniqueRules().Count;
        }

        public void DeleteByCharacterId(Id charId)
        {
            this.DeleteByRelatedId(charId);
        }

        public async Task DeleteByCharacterIdAsync(Id charId)
        {
            this.DeleteByCharacterId(charId);
        }
    }
}