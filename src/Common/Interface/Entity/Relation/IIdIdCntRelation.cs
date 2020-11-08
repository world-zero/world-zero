using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;

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