using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface ICommentRepo
        : IEntityRelationRepo
          <
            IComment,
            Id,
            int,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
          >
    {
        /// <summary>
        /// `Delete()` all comments on a specific praxis. 
        /// </summary>
        void DeleteByPraxisId(Id praxisId);
        Task DeleteByPraxisIdAsync(Id praxisId);

        /// <summary>
        /// `Delete()` all comments by a specific character. 
        /// </summary>
        void DeleteByCharacterId(Id charId);
        Task DeleteByCharacterIdAsync(Id charId);
    }
}