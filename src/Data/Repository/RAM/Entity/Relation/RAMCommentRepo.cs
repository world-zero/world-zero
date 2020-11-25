using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="ICommentRepo"/>
    public class RAMCommentRepo
        : IRAMEntityRelationRepo
          <
            Comment,
            Id,
            int,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
          >,
          ICommentRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Comment(new Id(1), new Id(2), "sdf");
            return a.GetUniqueRules().Count;
        }

        public void DeleteByPraxisId(Id praxisId)
        {
            this.DeleteByLeftId(praxisId);
        }

        public async Task DeleteByPraxisIdAsync(Id praxisId)
        {
            this.DeleteByLeftId(praxisId);
        }

        public void DeleteByCharacterId(Id charId)
        {
            this.DeleteByRightId(charId);
        }

        public async Task DeleteByCharacterIdAsync(Id charId)
        {
            this.DeleteByRightId(charId);
        }
    }
}