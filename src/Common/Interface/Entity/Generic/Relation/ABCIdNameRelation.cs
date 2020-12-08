using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="ABCEntityRelation"/>
    public abstract class ABCIdNameRelation
        : ABCEntityRelation<Id, int, Name, string>
    {
        public ABCIdNameRelation(Id leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public ABCIdNameRelation(Id id, Id leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override RelationDTO<Id, int, Name, string> GetDTO()
        {
            return new RelationDTO<Id, int, Name, string>(
                this.LeftId,
                this.RightId
            );
        }
    }
}