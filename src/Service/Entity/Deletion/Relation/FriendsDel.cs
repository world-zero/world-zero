using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Deletion;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityDel"/>
    public class FriendDel : IEntityDel<Friend, Id, int>
    {
        public FriendDel(IFriendRepo repo)
            : base(repo)
        { }
    }
}