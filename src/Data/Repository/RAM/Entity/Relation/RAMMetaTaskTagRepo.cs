using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskTagRepo"/>
    public class RAMMetaTaskTagRepo
        : IRAMEntityRelationRepo
          <
            MetaTaskTag,
            Id,
            int,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
          >,
          IMetaTaskTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new MetaTaskTag(new Id(3), new Name("sdf"));
            return a.GetUniqueRules().Count;
        }
    }
}