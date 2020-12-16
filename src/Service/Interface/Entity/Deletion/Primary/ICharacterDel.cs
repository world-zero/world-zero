using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel{TEntity, TId, TBuiltIn}"/>
    public interface ICharacterDel
        : IEntityDel<ICharacter, Id, int>
    {
        void DeleteByPlayer(IPlayer p);
        void DeleteByPlayer(Id playerId);
        Task DeleteByPlayerAsync(IPlayer p);
        Task DeleteByPlayerAsync(Id playerId);
    }
}