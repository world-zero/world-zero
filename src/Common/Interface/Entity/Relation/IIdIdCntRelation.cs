using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class IIdIdCntRelation
        : IEntityRelationCnt<Id, int, Id, int>
    {
        public IIdIdCntRelation(Id leftId, Id rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public IIdIdCntRelation(Id id, Id leftId, Id rightId, int cnt=1)
            : base(id, leftId, rightId, cnt)
        { }

        public override IRelationDTO<Id, int, Id, int> GetDTO()
        {
            return new IdIdDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}