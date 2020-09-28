using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class IIdNameCntRelation
        : IEntityRelationCnt<Id, int, Name, string>
    {
        public IIdNameCntRelation(Id leftId, Name rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public IIdNameCntRelation(Id id, Id leftId, Name rightId, int cnt=1)
            : base(id, leftId, rightId, cnt)
        { }

        public override IRelationDTO<Id, int, Name, string> GetDTO()
        {
            return new IdNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}