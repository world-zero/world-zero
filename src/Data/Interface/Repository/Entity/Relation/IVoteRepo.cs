using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IVoteRepo
        : IEntityRelationRepo
          <
            Vote,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >
    { }
}