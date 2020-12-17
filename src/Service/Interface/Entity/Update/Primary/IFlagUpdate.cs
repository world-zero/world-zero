using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface IFlagUpdate : IEntityUpdate<IFlag, Name, string>
    {
        void AmendDescription(IFlag f, string newDesc);
        void AmendDescription(Name flagId, string newDesc);
        Task AmendDescriptionAsync(IFlag f, string newDesc);
        Task AmendDescriptionAsync(Name flagId, string newDesc);

        void AmendPenalty(IFlag f, PointTotal newPenalty);
        void AmendPenalty(Name flagId, PointTotal newPenalty);
        Task AmendPenaltyAsync(IFlag f, PointTotal newPenalty);
        Task AmendPenaltyAsync(Name flagId, PointTotal newPenalty);

        void AmendIsFlagPenalty(IFlag f, bool newIsFlagPenalty);
        void AmendIsFlagPenalty(Name flagId, bool newIsFlagPenalty);
        Task AmendIsFlagPenaltyAsync(IFlag f, bool newIsFlagPenalty);
        Task AmendIsFlagPenaltyAsync(Name flagId, bool newIsFlagPenalty);
    }
}