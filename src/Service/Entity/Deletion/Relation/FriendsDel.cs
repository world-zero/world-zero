using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntitySelfRelationDel"/>
    public class FriendDel : IEntitySelfRelationDel
        <Friend, Character, Id, int, RelationDTO<Id, int, Id, int>>
    {
        public FriendDel(IFriendRepo repo)
            : base(repo)
        { }
    }
}