using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IFriendRepo
        : IEntitySelfRelationRepo
          <
            Friend,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >
    {
        /// <summary>
        /// `Delete()` all friends associated with the supplied character ID. 
        /// </summary>
        void DeleteByCharacterId(Id charId);
        Task DeleteByCharacterIdAsync(Id charId);
    }
}