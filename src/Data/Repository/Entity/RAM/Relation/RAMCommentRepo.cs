using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ICommentRepo"/>
    /// <remarks>
    /// As this repo is not a production-level tool (database repos fill that
    /// shoe), this will *not* allow multiple comments from the same character
    /// onto a praxis. This is because the third component of the Comment
    /// identifier, SubmissionCount, is not addressed.
    /// </remarks>
    public class RAMCommentRepo
        : IRAMEntityRelationRepo
          <
            Comment,
            Id,
            int,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
          >,
          ICommentRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Comment(new Id(1), new Id(2), "sdf");
            return a.GetUniqueRules().Count;
        }
    }
}