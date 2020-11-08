using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IMetaTaskTagRepo
        : IEntityRelationRepo
          <
            MetaTaskTag,
            Id,
            int,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
          >
    { }
}