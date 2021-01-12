using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// An Era is a configuration file for the game at any given time. If a
    /// start date is not supplied during initialization, then the time of
    /// initialization will be used.
    /// </summary>
    public interface IEraDTO : IEntityDTO<Name, string>
    {
        PastDate StartDate { get; }

        PastDate EndDate { get; }

        /// <summary>
        /// This is the number of levels someone can be under for a task to be
        /// allowed to submit a praxis for it.
        /// </summary>
        /// <remarks>
        /// An example of this would be how a character of level 3 would be
        /// able to submit a praxis for a task of level 5 if `TaskLevelBuffer`
        /// is 2.
        /// </remarks>
        Level TaskLevelBuffer { get; }

        /// <summary>
        /// This is the max number of praxises a character can have in progress
        /// and active, cummulative.
        /// </summary>
        int MaxPraxises { get; }

        /// <summary>
        /// This is the max number of times a character can complete any given
        /// task.
        /// </summary>
        int MaxTaskCompletion { get; }

        /// <summary>
        /// This is the max number of times a character can complete any given
        /// task iff they have the Reiterator ability.
        /// </summary>
        int MaxTaskCompletionReiterator { get; }
    }
}