using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Relation
{
    /// <inheritdoc cref="IEntityUpdate{TEntity}"/>
    public interface IPraxisParticipantUpdate : IEntityUpdate<IPraxisParticipant, Id, int>
    { }
}