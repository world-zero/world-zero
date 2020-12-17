using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IIdStatusedEntityUpdate{TEntity}"/>
    public interface IMetaTaskUpdate : IIdStatusedEntityUpdate<IMetaTask>
    {
        void AmendDescription(IMetaTask mt, string newDesc);
        void AmendDescription(Id mtId, string newDesc);
        Task AmendDescriptionAsync(IMetaTask mt, string newDesc);
        Task AmendDescriptionAsync(Id mtId, string newDesc);

        void AmendBonus(IMetaTask mt, PointTotal newBonus);
        void AmendBonus(Id mtId, PointTotal newBonus);
        Task AmendBonusAsync(IMetaTask mt, PointTotal newBonus);
        Task AmendBonusAsync(Id mtId, PointTotal newBonus);

        void AmendIsFlatBonus(IMetaTask mt, bool newIsMetaTaskBonus);
        void AmendIsFlatBonus(Id mtId, bool newIsMetaTaskBonus);
        Task AmendIsFlatBonusAsync(IMetaTask mt, bool newIsMetaTaskBonus);
        Task AmendIsFlatBonusAsync(Id mtId, bool newIsMetaTaskBonus);
    }
}