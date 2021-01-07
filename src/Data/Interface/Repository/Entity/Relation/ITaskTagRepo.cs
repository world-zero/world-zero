using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface ITaskTagRepo
        : ITaggedEntityRepo
          <
            ITaskTag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >
    {
        /// <summary>
        /// `Delete()` all relations associated with the supplied task ID.
        /// </summary>
        void DeleteByTaskId(Id taskId);
        Task DeleteByTaskIdAsync(Id taskId);
    }
}