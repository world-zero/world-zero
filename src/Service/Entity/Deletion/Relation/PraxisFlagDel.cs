using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityDel"/>
    public class PraxisFlagDel : ABCFlaggedEntityDel
    <
        UnsafePraxisFlag,
        UnsafePraxis,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >
    {
        public PraxisFlagDel(IPraxisFlagRepo repo)
            : base(repo)
        { }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByPraxis(UnsafePraxis praxis)
        {
            this.AssertNotNull(praxis, "praxis");
            this.DeleteByLeft(praxis.Id);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByPraxis(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            this.DeleteByLeft(praxisId);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async Task DeleteByPraxisAsync(UnsafePraxis praxis)
        {
            this.AssertNotNull(praxis, "praxis");
            await Task.Run(() => this.DeleteByLeft(praxis.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async Task DeleteByPraxisAsync(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await Task.Run(() => this.DeleteByLeft(praxisId));
        }
    }
}