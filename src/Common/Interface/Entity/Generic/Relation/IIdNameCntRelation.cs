using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    /// <inheritdoc cref="IEntityCntRelation"/>
    public interface IIdNameCntRelation
        : IEntityCntRelation<Id, int, Name, string>
    { }
}