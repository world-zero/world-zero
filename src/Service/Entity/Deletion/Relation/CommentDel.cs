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
    /// <inheritdoc cref="ICommentDel"/>
    public class CommentDel :
        ABCEntityRelationCntDel
        <
            IComment,
            IPraxis, Id, int,
            ICharacter, Id, int,
            CntRelationDTO<Id, int, Id, int>
        >, ICommentDel
    {
        protected ICommentRepo _commentRepo
        { get { return (ICommentRepo) this._repo; } }

        protected readonly CommentFlagDel _cfDel;

        public CommentDel(ICommentRepo repo, CommentFlagDel cfDel)
            : base(repo)
        {
            this.AssertNotNull(cfDel, "cfDel");
            this._cfDel = cfDel;
        }

        public override void Delete(Id commentId)
        {
            void f(Id id)
            {
                this._cfDel.DeleteByComment(id);
                base.Delete(id);
            }
            this.Transaction<Id>(f, commentId, true);
        }

        public void DeleteByPraxis(IPraxis p)
        {
            this.AssertNotNull(p, "p");
            this.DeleteByPraxis(p.Id);
        }

        public void DeleteByPraxis(Id praxisId)
        {
            this.Transaction<Id>(this._commentRepo.DeleteByPraxisId, praxisId);
        }

        public async Task DeleteByPraxisAsync(IPraxis p)
        {
            this.AssertNotNull(p, "P");
            await Task.Run(() => this.DeleteByPraxis(p));
        }

        public async Task DeleteByPraxisAsync(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await Task.Run(() => this.DeleteByPraxis(praxisId));
        }

        public void DeleteByCharacter(ICharacter c)
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

        public async Task DeleteByCharacterAsync(ICharacter c)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.DeleteByCharacter(c));
        }

        public async Task DeleteByCharacterAsync(Id charId)
        {
            this.AssertNotNull(charId, "charId");
            await Task.Run(() => this.DeleteByCharacter(charId));
        }
    }
}