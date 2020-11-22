using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    public class CommentDel : IEntityDel<Comment, Id, int>
    {
        public CommentDel(ICommentRepo repo)
            : base(repo)
        { }
    }
}