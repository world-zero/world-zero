using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IFriendDel"/>
    public class FriendDel : ABCEntitySelfRelationDel
        <IFriend, ICharacter, Id, int, RelationDTO<Id, int, Id, int>>,
        IFriendDel
    {
        protected IFriendRepo _friendRepo
        { get { return (IFriendRepo) this._relRepo; } }

        public FriendDel(IFriendRepo repo)
            : base(repo)
        { }

        public void DeleteByCharacter(ICharacter c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByCharacter(c.Id);
        }

        public void DeleteByCharacter(Id CharacterId)
        {
            this.Transaction<Id>(
                this._friendRepo.DeleteByCharacterId,
                CharacterId
            );
        }

        public async Task DeleteByCharacterAsync(ICharacter p)
        {
            this.AssertNotNull(p, "P");
            await Task.Run(() => this.DeleteByCharacter(p));
        }

        public async Task DeleteByCharacterAsync(Id charId)
        {
            this.AssertNotNull(charId, "charId");
            await Task.Run(() => this.DeleteByCharacter(charId));
        }
    }
}