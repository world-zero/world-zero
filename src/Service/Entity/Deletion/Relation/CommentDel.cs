using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityDel"/>
    public class CommentDel : ABCEntityDel<UnsafeComment, Id, int>
    {
        protected ICommentRepo _commentRepo
        { get { return (ICommentRepo) this._repo; } }

        public CommentDel(ICommentRepo repo)
            : base(repo)
        { }

        public void DeleteByPraxis(UnsafePraxis p)
        {
            this.AssertNotNull(p, "p");
            this.DeleteByPraxis(p.Id);
        }

        public void DeleteByPraxis(Id praxisId)
        {
            this.Transaction<Id>(this._commentRepo.DeleteByPraxisId, praxisId);
        }

        public async Task DeleteByPraxisAsync(UnsafePraxis p)
        {
            this.AssertNotNull(p, "P");
            await Task.Run(() => this.DeleteByPraxis(p));
        }

        public async
        Task DeleteByPraxisAsync(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await Task.Run(() => this.DeleteByPraxis(praxisId));
        }

        public void DeleteByCharacter(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByCharacter(c.Id);
        }

        public void DeleteByCharacter(Id CharacterId)
        {
            this.Transaction<Id>(
                this._commentRepo.DeleteByCharacterId,
                CharacterId
            );
        }

        public async
        Task DeleteByCharacterAsync(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.DeleteByCharacter(c));
        }

        public async
        Task DeleteByCharacterAsync(Id charId)
        {
            this.AssertNotNull(charId, "charId");
            await Task.Run(() => this.DeleteByCharacter(charId));
        }
    }
}