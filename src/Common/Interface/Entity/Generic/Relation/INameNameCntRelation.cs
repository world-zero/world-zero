using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class INameNameCntRelation
        : IEntityRelationCnt<Name, string, Name, string>
    {
        public INameNameCntRelation(Name leftId, Name rightId, int cnt=1)
            : base(leftId, rightId, cnt)
        { }

        public INameNameCntRelation(Id id, Name leftId, Name rightId, int cnt=1)
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