using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="ABCEntityRelation"/>
    public abstract class ABCNameNameRelation
        : ABCEntityRelation<Name, string, Name, string>
    {
        public ABCNameNameRelation(Name leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public ABCNameNameRelation(Id id, Name leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override RelationDTO<Name, string, Name, string> GetDTO()
        {
            return new RelationDTO<Name, string, Name, string>(
                this.LeftId,
                this.RightId
            );
        }
    }
}