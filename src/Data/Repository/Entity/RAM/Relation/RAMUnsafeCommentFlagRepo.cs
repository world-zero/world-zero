using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IUnsafeCommentFlagRepo"/>
    public class RAMUnsafeCommentFlagRepo
        : IRAMFlaggedEntityRepo
          <
            UnsafeCommentFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          IUnsafeCommentFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeCommentFlag(new Id(1), new Name("fasdff"));
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