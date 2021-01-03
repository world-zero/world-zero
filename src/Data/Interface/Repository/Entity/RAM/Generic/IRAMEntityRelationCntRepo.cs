using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    /// <inheritdoc cref="IEntityRelationCntRepo"/>
    public abstract class IRAMEntityRelationCntRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IRAMEntityRelationRepo
          <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
          >,
          IEntityRelationCntRepo
          <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
          >
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelationCnt
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TRelationDTO : CntRelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        public IRAMEntityRelationCntRepo()
            : base()
        { }

        public IEnumerable<TEntityRelation> GetByPartialDTO(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            if (dto == null)
                throw new ArgumentNullException("dto");

            IEnumerable<TEntityRelation> r =
                from erTemp in this._saved.Values
                let er = this.TEntityCast(erTemp)
                where dto.LeftId == er.LeftId
                where dto.RightId == er.RightId
                select er;

            if (r.Count() == 0)
                throw new ArgumentException("dto has no matching entities.");

            return r;
        }

        public int GetNextCount(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            int c = 0;
            int savedC = 0;
            int stagedC = 0;

            try
            {
                IEnumerable<int> saved =
                    from val in this._saved.Values
                    let value = this.TEntityCast(val)
                    where value.LeftId == dto.LeftId
                    where value.RightId == dto.RightId
                    select value.Count;
                savedC = saved.Max();
            }
            catch (InvalidOperationException)
            { }

            try
            {
                IEnumerable<int> staged =
                    from val in this._staged.Values
                    let value = this.TEntityCast(val)
                    where value.LeftId == dto.LeftId
                    where value.RightId == dto.RightId
                    select value.Count;
                stagedC = staged.Max();
            }
            catch (InvalidOperationException)
            { }

            if (savedC > c)
                c = savedC;
            if (stagedC > c)
                c = stagedC;
            return c+1;
        }

        public void DeleteByPartialDTO(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            // Yes, it would be more effecient to check staged first, but this
            // way will reveal bugs more easily.
            if (dto == null)
                throw new ArgumentNullException("dto");

            IEnumerable<TEntityRelation> ers = 
                from erTemp in this._saved.Values
                let er = this.TEntityCast(erTemp)
                where er.LeftId == dto.LeftId
                where er.RightId == dto.RightId
                select er;

            foreach (TEntityRelation er in ers)
                this.Delete(er.Id);

            // It is possible to delete a staged-only entity, and there are
            // some entity types that do not have an ID set until Save(), so it
            // is necessary to check the entity with context for the ID.
            var ids = new List<Id>();
            foreach (KeyValuePair<object, object> p in this._staged)
            {
                Id tempId;
                TEntityRelation er;
                try
                {
                    tempId = this.TIdCast(p.Key);
                    er = this.TEntityCast(p.Value);
                }
                catch (ArgumentException e)
                { throw new InvalidOperationException("A cast failed.", e); }

                if ( (er != null) && (er.LeftId == dto.LeftId) )
                {
                    if ( (er != null) && (er.RightId == dto.RightId) )
                    {
                        ids.Add(tempId);
                    }
                }
            }

            foreach (Id tempId in ids)
                this.Delete(tempId);
        }

        public async Task DeleteByPartialDTOAsync(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            this.DeleteByPartialDTO(dto);
        }

        public async Task<int> GetNextCountAsync(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            return this.GetNextCount(dto);
        }
    }
}