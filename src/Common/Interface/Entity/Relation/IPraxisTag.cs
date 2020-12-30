using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="ITaggedEntity"/>
    /// <summary>
    /// This relation maps a Praxis's ID to a Tag's ID,
    /// signifying that the praxis has tag X.
    /// <br />
    /// Left relation: `PraxisId`
    /// <br />
    /// Right relation: `TagId`
    /// </summary>
    public interface IPraxisTag : ITaggedEntity<Id, int>
    {
        /// <summary>
        /// PraxisId is a wrapper for LeftId.
        /// </summary>
        Id PraxisId { get; }
    }
}
