using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface IStatusUpdate : IEntityUpdate<IStatus, Name, string>
    {
        void AmendDescription(IStatus s, string newDesc);
        void AmendDescription(Name statusId, string newDesc);
        Task AmendDescriptionAsync(IStatus s, string newDesc);
        Task AmendDescriptionAsync(Name statusId, string newDesc);
    }
}