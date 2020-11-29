using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="ICommentRepo"/>
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