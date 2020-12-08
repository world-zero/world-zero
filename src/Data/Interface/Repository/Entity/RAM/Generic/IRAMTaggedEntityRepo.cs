using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    public abstract class IRAMTaggedEntityRepo
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
        ITaggedEntityRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRelationDTO
        >
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TEntityRelation : ABCEntityRelation
            <TLeftId, TLeftBuiltIn, Name, string>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, Name, string>

    {
        public void DeleteByTagId(Name tagId)
        {
            this.DeleteByRightId(tagId);
        }

        public async Task DeleteByTagIdAsync(Name tagId)
        {
            this.DeleteByTagId(tagId);
        }
    }
}