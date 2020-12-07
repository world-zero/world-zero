using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityDel"/>
    public class CommentDel : IEntityDel<Comment, Id, int>
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

        public async System.Threading.Tasks.Task DeleteByPraxisAsync(UnsafePraxis p)
        {
            this.AssertNotNull(p, "P");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxis(p));
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisAsync(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxis(praxisId));
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
        System.Threading.Tasks.Task DeleteByCharacterAsync(UnsafeCharacter c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByCharacter(c));
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