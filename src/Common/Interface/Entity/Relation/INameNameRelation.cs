using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Dual;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation">
    public abstract class INameNameRelation
        : IEntityRelation<Name, string, Name, string>
    {
        public INameNameRelation(Name leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public INameNameRelation(Id id, Name leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override IDualDTO<Name, string, Name, string> GetDTO()
        {
            return new NameNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}