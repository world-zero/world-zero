using System;
using System.Threading.Tasks;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.RAM.Entity.Relation
{
    public abstract class IRAMEntitySelfRelationRepo
    <TEntityRelation, TId, TBuiltIn, TRelationDTO>
        : IRAMEntityRelationRepo
          <TEntityRelation, TId, TBuiltIn, TId, TBuiltIn, TRelationDTO>,
          IEntitySelfRelationRepo<TEntityRelation, TId, TBuiltIn, TRelationDTO>
        where TId : ISingleValueObject<TBuiltIn>
        where TEntityRelation : IEntitySelfRelation
            <TId, TBuiltIn>
        where TRelationDTO : RelationDTO
            <TId, TBuiltIn, TId, TBuiltIn>
    {
        public IRAMEntitySelfRelationRepo()
            : base()
        { }

        public void DeleteByRelatedId(TId id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            this.DeleteByLeftId(id);
            this.DeleteByRightId(id);
        }

        public async Task DeleteByRelatedIdAsync(TId id)
        {
            this.DeleteByRelatedId(id);
        }
    }
}