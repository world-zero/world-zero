using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.Interface.Entity.Relation
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

        public override IRelationDTO<Name, string, Name, string> GetDTO()
        {
            return new NameNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}