using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Dual;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IEntityRelation">
    public abstract class IIdNameRelation
        : IEntityRelation<Id, int, Name, string>
    {
        public IIdNameRelation(Id leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public IIdNameRelation(Id id, Id leftId, Name rightId)
            : base(id, leftId, rightId)
        { }

        public override IDualDTO<Id, int, Name, string> GetDTO()
        {
            return new IdNameDTO(
                this.LeftId,
                this.RightId
            );
        }
    }
}