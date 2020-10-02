using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="ICommentFlagRepo"/>
    public class RAMCommentFlagRepo
        : IRAMEntityRelationRepo
          <
            CommentFlag,
            Id,
            int,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
          >,
          ICommentFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new CommentFlag(new Id(1), new Name("fasdff"));
            return a.GetUniqueRules().Count;
        }
    }
}