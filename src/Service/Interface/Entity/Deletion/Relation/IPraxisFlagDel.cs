using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IFlaggedEntityDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRelationDTO}"/>
    public interface IPraxisFlagDel
        : IFlaggedEntityDel
        <
            IPraxisFlag, IPraxis, Id, int,
            RelationDTO<Id, int, Name, string>
        >
    {
        void DeleteByPraxis(IPraxis p);
        void DeleteByPraxis(Id praxisId);
        Task DeleteByPraxisAsync(IPraxis p);
        Task DeleteByPraxisAsync(Id praxisId);
    }
}