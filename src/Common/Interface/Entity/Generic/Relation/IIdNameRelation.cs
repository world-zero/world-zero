using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public interface IIdNameRelation
        : IEntityRelation<Id, int, Name, string>
    { }
}