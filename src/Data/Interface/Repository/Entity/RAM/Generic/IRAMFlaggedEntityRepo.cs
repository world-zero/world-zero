using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    public abstract class IRAMFlaggedEntityRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRelationDTO
    >
        : IRAMEntityRelationRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            Name,
            string,
            TRelationDTO
        >,
        IFlaggedEntityRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRelationDTO
        >
        where TLeftId : ABCSingleValueObject<TLeftBuiltIn>
        where TEntityRelation : class, IEntityRelation
            <TLeftId, TLeftBuiltIn, Name, string>
        where TRelationDTO : NoIdRelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>

    {
        public void DeleteByFlagId(Name flagId)
        {
            this.DeleteByRightId(flagId);
        }

        public async Task DeleteByFlagIdAsync(Name flagId)
        {
            this.DeleteByFlagId(flagId);
        }
    }
}