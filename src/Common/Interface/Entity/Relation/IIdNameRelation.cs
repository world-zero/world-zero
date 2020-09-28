using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class IIdNameRelation
        : IEntityRelation<Id, int, Name, string>
    {
        public IIdNameRelation(Id leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public IIdNameRelation(Id id, Id leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override IRelationDTO<Id, int, Name, string> GetDTO()
        {
            return new IdNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}