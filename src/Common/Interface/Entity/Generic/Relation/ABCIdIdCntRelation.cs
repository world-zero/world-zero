using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="UnsafeIEntityRelation"/>
    public abstract class UnsafeIIdIdCntRelation
        : UnsafeIEntityRelationCnt<Id, int, Id, int>
    {
        public UnsafeIIdIdCntRelation(Id leftId, Id rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public UnsafeIIdIdCntRelation(Id id, Id leftId, Id rightId, int cnt=1)
            : base(id, leftId, rightId, cnt)
        { }

        public override RelationDTO<Id, int, Id, int> GetDTO()
        {
            return new CntRelationDTO<Id, int, Id, int>(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }
    }
}