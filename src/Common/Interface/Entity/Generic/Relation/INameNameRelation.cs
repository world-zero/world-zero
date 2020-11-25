using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class INameNameRelation
        : IEntityRelation<Name, string, Name, string>
    {
        public INameNameRelation(Name leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public INameNameRelation(Id id, Name leftId, Name rightId)
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