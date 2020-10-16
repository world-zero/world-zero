using System;
using System.Collections.Generic;
using System.Linq;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.RAM.Entity.Relation
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
            int c = 1;
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
    }
}