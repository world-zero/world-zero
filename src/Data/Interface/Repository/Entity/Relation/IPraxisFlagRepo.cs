using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IPraxisFlagRepo
        : IFlaggedEntityRepo
          <
            IPraxisFlag,
            Id,
            int,
            NoIdRelationDTO<Id, int, Name, string>
          >
    {
        /// <summary>
        /// `Delete()` all relations associated with the supplied praxis ID.
        /// </summary>
        void DeleteByPraxisId(Id praxisId);
        Task DeleteByPraxisIdAsync(Id praxisId);
    }
}