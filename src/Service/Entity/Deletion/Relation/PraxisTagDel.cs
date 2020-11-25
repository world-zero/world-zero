using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityDel"/>
    public class PraxisTagDel : ITaggedEntityDel
    <
        PraxisTag,
        Praxis,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >
    {
        public PraxisTagDel(IPraxisTagRepo repo)
            : base(repo)
        { }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByPraxis(Praxis praxis)
        {
            this.AssertNotNull(praxis, "praxis");
            this.DeleteByLeftId(praxis.Id);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByPraxis(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            this.DeleteByLeftId(praxisId);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async
        System.Threading.Tasks.Task DeleteByPraxisAsync(Praxis praxis)
        {
            this.AssertNotNull(praxis, "praxis");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByLeftId(praxis.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async
        System.Threading.Tasks.Task DeleteByPraxisAsync(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByLeftId(praxisId));
        }
    }
}