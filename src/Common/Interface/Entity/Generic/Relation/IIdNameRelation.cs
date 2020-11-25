using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
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

        public override RelationDTO<Id, int, Name, string> GetDTO()
        {
            return new RelationDTO<Id, int, Name, string>(
                this.LeftId,
                this.RightId
            );
        }
    }
}