using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntitySelfRelationDel"/>
    public class FoeDel : ABCEntitySelfRelationDel
        <UnsafeFoe, UnsafeCharacter, Id, int, RelationDTO<Id, int, Id, int>>
    {
        protected IFoeRepo _foeRepo
        { get { return (IFoeRepo) this._relRepo; } }

        public FoeDel(IFoeRepo repo)
            : base(repo)
        { }

        public void DeleteByCharacter(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByCharacter(c.Id);
        }

        public void DeleteByCharacter(Id CharacterId)
        {
            this.Transaction<Id>(
                this._foeRepo.DeleteByCharacterId,
                CharacterId
            );
        }

        public async
        Task DeleteByCharacterAsync(UnsafeCharacter p)
        {
            this.AssertNotNull(p, "P");
            await Task.Run(() => this.DeleteByCharacter(p));
        }

        public async
        Task DeleteByCharacterAsync(Id charId)
        {
            this.AssertNotNull(charId, "charId");
            await Task.Run(() => this.DeleteByCharacter(charId));
        }
    }
}