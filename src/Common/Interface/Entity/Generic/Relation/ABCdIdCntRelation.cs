using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IIdIdCntRelation"/>
    public abstract class ABCIdIdCntRelation
        : ABCEntityCntRelation<Id, int, Id, int>,
          IIdIdCntRelation
    {
        public ABCIdIdCntRelation(Id leftId, Id rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public ABCIdIdCntRelation(Id id, Id leftId, Id rightId, int cnt=1)
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