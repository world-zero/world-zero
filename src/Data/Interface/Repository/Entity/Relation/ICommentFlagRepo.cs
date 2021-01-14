using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
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
            NoIdRelationDTO<Id, int, Name, string>
          >
    {
        IEnumerable<ICommentFlag> GetByCommentId(Id commentId);
    }
}