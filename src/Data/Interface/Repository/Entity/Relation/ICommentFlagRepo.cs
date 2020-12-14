using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface ICommentFlagRepo
        : IFlaggedEntityRepo
          <
            ICommentFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >
    {
        /// <summary>
        /// `Delete()` all relations associated with the supplied comment ID.
        /// </summary>
        void DeleteByCommentId(Id commentId);
        Task DeleteByCommentIdAsync(Id commentId);
    }
}