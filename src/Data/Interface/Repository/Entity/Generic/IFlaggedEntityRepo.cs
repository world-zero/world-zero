using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Primary.Generic
{
    /// <remarks>
    /// As you may have noticed, relational entities that flag should have the
    /// flag ID as the right relation.
    /// </remarks>
    public interface IFlaggedEntityRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRelationDTO
    >
        : IEntityRelationRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            Name,
            string,
            TRelationDTO
        >
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TEntityRelation : IEntityRelation
            <TLeftId, TLeftBuiltIn, Name, string>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>
    {
        // NOTE: I am leaving the GetByFlagId until I work on the reading
        // service classes.

        void DeleteByFlagId(Name flagId);
        Task DeleteByFlagIdAsync(Name flagId);
    }
}