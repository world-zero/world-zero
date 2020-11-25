using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
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