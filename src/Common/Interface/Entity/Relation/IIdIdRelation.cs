using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class IIdIdRelation
        : IEntityRelation<Id, int, Id, int>
    {
        public IIdIdRelation(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public IIdIdRelation(Id id, Id leftId, Id rightId)
            : base(id, leftId, rightId)
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