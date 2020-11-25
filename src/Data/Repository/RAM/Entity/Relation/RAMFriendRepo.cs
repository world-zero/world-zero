using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IFriendRepo"/>
    public class RAMFriendRepo
        : IRAMEntitySelfRelationRepo
          <
            Friend,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >,
          IFriendRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Friend(new Id(3), new Id(2));
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