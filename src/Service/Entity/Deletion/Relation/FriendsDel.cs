using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntitySelfRelationDel"/>
    public class FriendDel : IEntitySelfRelationDel
        <Friend, Character, Id, int, RelationDTO<Id, int, Id, int>>
    {
        public FriendDel(IFriendRepo repo)
            : base(repo)
        { }

        // TODO: delete by character
    }
}