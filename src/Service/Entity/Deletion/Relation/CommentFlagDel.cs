using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="ICommentFlagDel"/>
    public class CommentFlagDel : ABCEntityRelateRelationalDel
    <
        ICommentFlag,
        IComment, Id, int,
            IComment, Id, int, Id, int, CntRelationDTO<Id, int, Id, int>,
        IFlag, Name, string,
        RelationDTO<Id, int, Name, string>
    >, ICommentFlagDel
    {
        protected ICommentFlagRepo _cfRepo
        { get { return (ICommentFlagRepo) this._repo; } }

        public CommentFlagDel(
            IEntityRelationRepo
            <
                ICommentFlag,
                Id,
                int,
                Name,
                string,
                RelationDTO<Id, int, Name, string>
            >
            repo
        )
            : base(repo)
        { }

        public void DeleteByComment(IComment c)
        {
            this.AssertNotNull(c, "c");
            this.DeleteByComment(c.Id);
        }

        public void DeleteByComment(Id commentId)
        {
            void f(Id id)
            {
                IEnumerable<ICommentFlag> cfs;
                try
                {
                    cfs = this._cfRepo.GetByCommentId(id);
                    cfs.Count();
                }
                catch (ArgumentException)
                { return; }
                try
                {
                    foreach (ICommentFlag cf in cfs)
                        this.Delete(cf.CommentId);
                }
                catch (ArgumentException e)
                { throw new ArgumentException("Could not complete the deletion.", e); }
            }
            this.Transaction<Id>(f, commentId, true);
        }

        public async Task DeleteByCommentAsync(IComment c)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.DeleteByComment(c));
        }

        public async Task DeleteByCommentAsync(Id commentId)
        {
            this.AssertNotNull(commentId, "commentId");
            await Task.Run(() => this.DeleteByComment(commentId));
        }
    }
}