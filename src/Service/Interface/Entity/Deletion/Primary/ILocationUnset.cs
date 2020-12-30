using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityUnset{TEntity, TId, TBuiltIn, TTEntity, TTId, TTBuiltIn}"/>
    public interface ILocationUnset
        : IEntityUnset<ILocation, Id, int, ICharacter, Id, int>
    { }
}