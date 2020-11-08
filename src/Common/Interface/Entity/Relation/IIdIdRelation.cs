using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;

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

        public override RelationDTO<Id, int, Id, int> GetDTO()
        {
            return new RelationDTO<Id, int, Id, int>(
                this.LeftId,
                this.RightId
            );
        }
    }
}