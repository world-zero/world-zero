using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface ITagUpdate : IEntityUpdate<ITag, Name, string>
    {
        void AmendDescription(ITag t, string newDesc);
        void AmendDescription(Name tagId, string newDesc);
        Task AmendDescriptionAsync(ITag t, string newDesc);
        Task AmendDescriptionAsync(Name tagId, string newDesc);
    }
}