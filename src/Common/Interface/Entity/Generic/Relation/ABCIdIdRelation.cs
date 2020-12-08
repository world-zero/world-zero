using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="ABCEntityRelation"/>
    public abstract class ABCIdIdRelation
        : ABCEntityRelation<Id, int, Id, int>
    {
        public ABCIdIdRelation(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public ABCIdIdRelation(Id id, Id leftId, Id rightId)
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