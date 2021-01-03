using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="INamedEntity"/>
    /// <summary>
    /// An Era is a configuration file for the game at any given time. If a
    /// start date is not supplied during initialization, then the time of
    /// initialization will be used.
    /// </summary>
    public interface IEra : INamedEntity
    {
        PastDate StartDate { get; }

        /// <remarks>
        /// Naturally, `EndDate` will be after `StartDate`.
        /// </remarks>
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
        /// <value>
        /// This must be at least 1.
        /// </value>
        int MaxPraxises { get; }

        /// <summary>
        /// This is the max number of times a character can complete any given
        /// task.
        /// </summary>
        /// <value>
        /// This must be at least 1 and no larger than `MaxTasksReiterator`.
        /// </value>
        int MaxTasks { get; }

        /// <summary>
        /// This is the max number of times a character can complete any given
        /// task iff they have the Reiterator ability.
        /// </summary>
        /// <value>
        /// This must be at least 1 and at least as large as `MaxTasks`.
        /// </value>
        int MaxTasksReiterator { get; }
    }
}