using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public interface IIdIdRelation
        : IEntityRelation<Id, int, Id, int>
    { }
}