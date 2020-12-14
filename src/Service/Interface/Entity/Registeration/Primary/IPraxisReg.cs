using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Primary
{
    /// <summary>
    /// Register the praxis and supplied participants.
    /// </summary>
    /// <remarks>
    /// When furthering development, be mindful about how Praxis needs a
    /// participant - both PraxisReg and PraxisParticipantReg really
    /// rely on this fact.
    /// <br />
    /// This will require at least one participant to be supplied upon
    /// registration.
    /// <br />
    /// This will ensure that a duel is between two characters.
    /// <br />
    /// A praxis can only be registered if it is Active or In Progress.
    /// <br />
    /// A praxis can only be registered if it has an Active task associated
    /// with it.
    /// <br />
    /// A praxis can only be registered if it has an Active meta task
    /// associated with it, if a meta task is supplied.
    /// <br />
    /// A praxis participant can only be registered if it's `PraxisId` is null.
    /// <br />
    /// When furthering development, be mindful about how PraxisReg needs a
    /// participant - both PraxisReg and PraxisParticipantReg rely on this
    /// fact.
    /// </remarks>
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public interface IPraxisReg
        : IEntityReg<IPraxis, Id, int>
    {
        // The inherited Register methods will just throw exceptions since this
        // also requires participants.

        IPraxis Register(IPraxis p, IPraxisParticipant pp);
        IPraxis Register(IPraxis p, List<IPraxisParticipant> pps);
        Task<IPraxis> RegisterAsync(IPraxis p, IPraxisParticipant pp);
        Task<IPraxis> RegisterAsync(IPraxis p, List<IPraxisParticipant> pps);
    }
}