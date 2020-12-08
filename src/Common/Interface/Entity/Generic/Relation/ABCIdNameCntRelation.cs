using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="ABCEntityRelation"/>
    public abstract class ABCIdNameCntRelation
        : ABCEntityRelationCnt<Id, int, Name, string>
    {
        public ABCIdNameCntRelation(Id leftId, Name rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public ABCIdNameCntRelation(Id id, Id leftId, Name rightId, int cnt=1)
            : base(id, leftId, rightId, cnt)
        { }

        public override RelationDTO<Id, int, Name, string> GetDTO()
        {
            return new CntRelationDTO<Id, int, Name, string>(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }
    }
}