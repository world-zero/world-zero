using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Update.Relation;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Relation
{
    /// <inheritdoc cref="ICommentFlagUpdate"/>
    public class CommentFlagUpdate
        : ABCEntityUpdate<ICommentFlag, Id, int>,
        ICommentFlagUpdate
    {
        public CommentFlagUpdate(ICommentFlagRepo cfRepo)
            : base(cfRepo)
        { }
    }
}