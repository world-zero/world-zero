using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ICommentFlagRepo"/>
    public class RAMCommentFlagRepo
        : IRAMIdNameRepo<CommentFlag>,
          ICommentFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new CommentFlag(new Id(1), new Name("fasdff"));
            return a.GetUniqueRules().Count;
        }
    }
}