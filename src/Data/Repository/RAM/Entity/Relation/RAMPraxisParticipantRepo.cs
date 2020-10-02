using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IPraxisParticipantRepo"/>
    /// <remarks>
    /// As this repo is not a production-level tool (database repos fill that
    /// shoe), this will *not* allow multiple submissions from the same
    /// character on the same praxis. This is because the third component of
    /// the PraxisParticipant identifier, SubmissionCount, is not addressed.
    /// </remarks>
    public class RAMPraxisParticipantRepo
        : IRAMEntityRelationRepo
          <
            PraxisParticipant,
            Id,
            int,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
          >,
          IPraxisParticipantRepo
    {
        protected override int GetRuleCount()
        {
            var a = new PraxisParticipant(new Id(3), new Id(2));
            return a.GetUniqueRules().Count;
        }
    }
}