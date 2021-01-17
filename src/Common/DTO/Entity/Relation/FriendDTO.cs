using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Relation
{
    /// <inheritdoc cref="IFriendDTO"/>
    public class FriendDTO : SelfRelationDTO<Id, int>, IFriendDTO
    {
        public Id FirstCharacterId  { get; }
        public Id SecondCharacterId { get; }

        public FriendDTO(
            Id id=null,
            Id charId0=null,
            Id charId1=null
        )
            : base(id, charId0, charId1)
        { }

        public override object Clone()
        {
            return new FriendDTO(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as FriendDTO;
            return
                c != null
                && base.Equals(c);
        }

        // GetHashCode is purposefully not overridden.
    }
}