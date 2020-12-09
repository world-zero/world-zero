using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public interface INameNameRelation
        : IEntityRelation<Name, string, Name, string>
    { }
}