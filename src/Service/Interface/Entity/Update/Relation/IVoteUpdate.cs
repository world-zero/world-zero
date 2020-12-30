using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Relation
{
    /// <remarks>
    /// If you choose to have this class actually amend votes, be very
    /// catious of when a vote's points are taken from the voter and added to
    /// the votee.
    /// </remarks>
    /// <inheritdoc cref="IEntityUpdate{TEntity, TId, TBuiltIn}"/>
    public interface IVoteUpdate : IEntityUpdate<IVote, Id, int>
    { }
}