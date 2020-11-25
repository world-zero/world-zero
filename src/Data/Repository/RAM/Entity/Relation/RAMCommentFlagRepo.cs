using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="ICommentFlagRepo"/>
    public class RAMCommentFlagRepo
        : IRAMFlaggedEntityRepo
          <
            CommentFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          ICommentFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new CommentFlag(new Id(1), new Name("fasdff"));
            return a.GetUniqueRules().Count;
        }

        public void DeleteByCommentId(Id commentId)
        {
            this.DeleteByLeftId(commentId);
        }

        public async Task DeleteByCommentIdAsync(Id commentId)
        {
            this.DeleteByCommentId(commentId);
        }
    }
}