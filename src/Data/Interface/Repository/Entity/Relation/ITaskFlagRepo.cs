using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface ITaskFlagRepo
        : IFlaggedEntityRepo
          <
            UnsafeTaskFlag,
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