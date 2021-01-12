using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Praxis is a entity for a tuple of the Praxis table.
    /// </summary>
    /// <remarks>
    /// As you would expect, validation of the Status is left to the Praxis
    /// registration class.
    /// </remarks>
    public interface IPraxisDTO : IIdStatusedDTO
    {
        Id TaskId { get; }
        PointTotal Points { get; }
        Id MetaTaskId { get; }

        /// <summary>
        /// This field will control whether or the the two participants are
        /// dueling. A duel requires two participants, and as the praxis is
        /// moved from being active to being retired, the person with the most
        /// points earned via Votes will get the points of the Praxis instead
        /// of both participants, had they been collaberating.
        /// </summary>
        bool AreDueling { get; }
    }
}