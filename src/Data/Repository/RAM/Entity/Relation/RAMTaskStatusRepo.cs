using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="ITaskStatusRepo"/>
    public class RAMTaskStatusRepo
        : IRAMEntityRelationRepo
          <
            TaskStatus,
            Id,
            int,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
          >,
          ITaskStatusRepo
    {
        protected override int GetRuleCount()
        {
            var a = new TaskStatus(new Id(3), new Name("sdf"));
            return a.GetUniqueRules().Count;
        }
    }
}