using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.DTO.Entity.Relation
{
    /// <summary>
    /// This relation maps a Praxis's ID to a Flag's ID,
    /// signifying that the praxis has flag X.
    /// <br />
    /// Left relation: `PraxisId`
    /// <br />
    /// Right relation: `FlagId`
    /// </summary>
    public interface IPraxisFlagDTO : IFlaggedDTO<Id, int>
    {
        /// <summary>
        /// PraxisId is a wrapper for LeftId.
        /// </summary>
        Id PraxisId { get; }
    }
}