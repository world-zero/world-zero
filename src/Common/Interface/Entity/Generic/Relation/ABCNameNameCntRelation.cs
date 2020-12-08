using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="UnsafeIEntityRelation"/>
    public abstract class UnsafeINameNameCntRelation
        : UnsafeIEntityRelationCnt<Name, string, Name, string>
    {
        public UnsafeINameNameCntRelation(Name leftId, Name rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public UnsafeINameNameCntRelation(Id id, Name leftId, Name rightId, int cnt=1)
            : base(id, leftId, rightId, cnt)
        { }

        public override RelationDTO<Name, string, Name, string> GetDTO()
        {
            return new CntRelationDTO<Name, string, Name, string>(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }
    }
}