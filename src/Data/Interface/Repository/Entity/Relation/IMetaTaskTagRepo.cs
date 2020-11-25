using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IMetaTaskTagRepo
        : ITaggedEntityRepo
          <
            MetaTaskTag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >
    {
        /// <summary>
        /// `Delete()` all relations associated with the supplied meta task ID.
        /// </summary>
        void DeleteByMetaTaskId(Id mtId);
        Task DeleteByMetaTaskIdAsync(Id mtId);
    }
}