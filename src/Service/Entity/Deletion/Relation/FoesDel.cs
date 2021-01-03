using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntitySelfRelationDel"/>
    public class FoeDel : IEntitySelfRelationDel
        <Foe, Character, Id, int, RelationDTO<Id, int, Id, int>>
    {
        protected IFoeRepo _foeRepo
        { get { return (IFoeRepo) this._relRepo; } }

        public FoeDel(IFoeRepo repo)
            : base(repo)
        { }

        public void DeleteByCharacter(Character c)
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
        System.Threading.Tasks.Task DeleteByCharacterAsync(Character p)
        {
            this.AssertNotNull(p, "P");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByCharacter(p));
        }

        public async
        System.Threading.Tasks.Task DeleteByCharacterAsync(Id charId)
        {
            this.AssertNotNull(charId, "charId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByCharacter(charId));
        }
    }
}