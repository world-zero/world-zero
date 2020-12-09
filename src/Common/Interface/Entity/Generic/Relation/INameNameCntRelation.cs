using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityCntRelation"/>
    public interface INameNameCntRelation
        : IEntityCntRelation<Name, string, Name, string>
    { }
}