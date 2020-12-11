using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IIdIdRelation"/>
    public abstract class IUnsafeIdIdRelation
        : IUnsafeEntityRelation<Id, int, Id, int>,
          IIdIdRelation
    {
        public IUnsafeIdIdRelation(Id leftId, Id rightId)
            : base(leftId, rightId)
        { }

        public IUnsafeIdIdRelation(Id id, Id leftId, Id rightId)
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