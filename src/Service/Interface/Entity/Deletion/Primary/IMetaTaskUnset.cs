using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityUnset{TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn}"/>
    public interface IMetaTaskUnset
        : IEntityUnset<IMetaTask, Id, int, IPraxis, Id, int>,
          IIdStatusedEntityDel<IMetaTask>
    {
        void DeleteByFaction(IFaction f);
        void DeleteByFaction(Name factionId);
        Task DeleteByFactionAsync(IFaction f);
        Task DeleteByFactionAsync(Name factionId);
    }
}