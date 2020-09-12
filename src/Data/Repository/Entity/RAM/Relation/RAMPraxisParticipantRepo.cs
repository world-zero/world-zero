using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IPraxisParticipantRepo">
    /// <remarks>
    /// As this repo is not a production-level tool (database repos fill that
    /// shoe), this will *not* allow multiple submissions from the same
    /// character on the same praxis. This is because the third component of
    /// the PraxisParticipant identifier, SubmissionCount, is not addressed.
    /// </remarks>
    public class RAMPraxisParticipantRepo
        : IRAMIdIdRepo<PraxisParticipant>,
          IPraxisParticipantRepo
    { }
}