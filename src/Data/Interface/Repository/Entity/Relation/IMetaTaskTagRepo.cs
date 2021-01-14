using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IMetaTaskTagRepo
        : ITaggedEntityRepo
          <
            IMetaTaskTag,
            Id,
            int,
            NoIdRelationDTO<Id, int, Name, string>
          >
    {
        /// <summary>
        /// `Delete()` all relations associated with the supplied meta task ID.
        /// </summary>
        void DeleteByMetaTaskId(Id mtId);
        Task DeleteByMetaTaskIdAsync(Id mtId);
    }
}