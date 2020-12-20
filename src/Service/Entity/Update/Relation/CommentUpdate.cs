using System;
using System.Threading.Tasks;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Update.Relation;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Relation
{
    /// <inheritdoc cref="ICommentUpdate"/>
    public class CommentUpdate
        : ABCEntityUpdate<IComment, Id, int>,
        ICommentUpdate
    {
        public CommentUpdate(ICommentRepo commentRepo)
            : base(commentRepo)
        { }

        public void AmendValue(IComment c, string newValue)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newValue, "newValue");
            void f<Id>(Id _)
            {
                UnsafeComment comment;
                try
                {
                    comment = (UnsafeComment) c;
                    comment.Value = newValue;
                }
                catch (InvalidCastException e)
                {
                    this.DiscardTransaction();
                    throw new InvalidOperationException("Could not cast, there is an outside implementation being supplied.", e);
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException("Could not complete the amendment.", e);
                }
                this._repo.Update(comment);
            }

            this.Transaction<Id>(f, new Id(0));
        }

        public void AmendValue(Id commentId, string newValue)
        {
            this.AssertNotNull(commentId, "commentId");
            this.AssertNotNull(newValue, "newValue");
            void f<Id>(Id _)
            {
                this.AmendValue(this._repo.GetById(commentId), newValue);
            }
            this.Transaction<Id>(f, new Id(0), true);
        }

        public async Task AmendValueAsync(IComment c, string newValue)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newValue, "newValue");
            await Task.Run(() => this.AmendValue(c, newValue));
        }

        public async Task AmendValueAsync(Id commentId, string newValue)
        {
            this.AssertNotNull(commentId, "commentId");
            this.AssertNotNull(newValue, "newValue");
            await Task.Run(() => this.AmendValueAsync(commentId, newValue));
        }
    }
}