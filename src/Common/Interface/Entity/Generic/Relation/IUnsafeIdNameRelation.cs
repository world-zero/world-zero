using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IIdNameRelation"/>
    public abstract class IUnsafeIdNameRelation
        : UnsafeIEntityRelation<Id, int, Name, string>,
          IIdNameRelation
    {
        public IUnsafeIdNameRelation(Id leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public IUnsafeIdNameRelation(Id id, Id leftId, Name rightId)
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