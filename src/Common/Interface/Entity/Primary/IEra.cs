using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IEraDTO"/>
    public interface IEra : INamedEntity
    {
        PastDate StartDate { get; }

        /// <remarks>
        /// Naturally, `EndDate` will be after `StartDate`.
        /// </remarks>
        PastDate EndDate { get; }

        Level TaskLevelBuffer { get; }

        /// <value>
        /// This must be at least 1.
        /// </value>
        int MaxPraxises { get; }

        /// <value>
        /// This must be at least 1 and no larger than `MaxTasksReiterator`.
        /// </value>
        int MaxTaskCompletion { get; }

        /// <value>
        /// This must be at least 1 and at least as large as `MaxTasks`.
        /// </value>
        int MaxTaskCompletionReiterator { get; }
    }
}