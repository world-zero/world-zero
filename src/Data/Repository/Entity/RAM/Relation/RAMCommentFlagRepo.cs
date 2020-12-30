using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ICommentFlagRepo"/>
    public class RAMCommentFlagRepo
        : IRAMFlaggedEntityRepo
          <
            ICommentFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          ICommentFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeCommentFlag(new Id(1), new Name("fasdff"));
            return a.GetUniqueRules().Count;
        }

        public IEnumerable<ICommentFlag> GetByCommentId(Id commentId)
            => this.GetByLeftId(commentId);
    }
}