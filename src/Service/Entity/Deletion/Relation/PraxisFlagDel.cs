using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IPraxisFlagDel"/>
    public class PraxisFlagDel : ABCFlaggedEntityDel
    <
        IPraxisFlag,
        IPraxis,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >, IPraxisFlagDel
    {
        public PraxisFlagDel(IPraxisFlagRepo repo)
            : base(repo)
        { }

        public void DeleteByPraxis(IPraxis praxis)
        {
            this.AssertNotNull(praxis, "praxis");
            this.DeleteByLeft(praxis.Id);
        }

        public void DeleteByPraxis(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            this.DeleteByLeft(praxisId);
        }

        public async Task DeleteByPraxisAsync(IPraxis praxis)
        {
            this.AssertNotNull(praxis, "praxis");
            await Task.Run(() => this.DeleteByLeft(praxis.Id));
        }

        public async Task DeleteByPraxisAsync(Id praxisId)
        {
            this.AssertNotNull(praxisId, "praxisId");
            await Task.Run(() => this.DeleteByLeft(praxisId));
        }
    }
}