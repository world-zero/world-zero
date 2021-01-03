using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Relation
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface ICommentUpdate : IEntityUpdate<IComment, Id, int>
    {
        void AmendValue(IComment c, string newValue);
        void AmendValue(Id commentId, string newValue);
        Task AmendValueAsync(IComment c, string newValue);
        Task AmendValueAsync(Id commentId, string newValue);
    }
}