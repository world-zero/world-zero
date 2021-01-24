using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <remarks>
    /// EndDate must be after StartDate.
    /// <br/>
    /// MaxPraxises must be at least 1.
    /// <br/>
    /// MaxTaskCompletion must be larger than 1 and no larger than
    /// MaxTasksReiterator.
    /// <br/>
    /// MaxTaskCompletionReiterator must be larger than 1 and at least as large
    /// as MaxTasksReiterator.
    /// </remarks>
    /// <inheritdoc cref="IEraDTO"/>
    public interface IEra : IEraDTO, INamedEntity
    { }
}