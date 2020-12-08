using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="UnsafeIEntityRelation"/>
    public abstract class UnsafeIIdIdRelation
        : UnsafeIEntityRelation<Id, int, Id, int>
    {
        public UnsafeIIdIdRelation(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public UnsafeIIdIdRelation(Id id, Id leftId, Id rightId)
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