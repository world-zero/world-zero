using WorldZero.Common.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Unspecified.Relation
{
    public class CntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>,
        IEntityCntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        public int Count { get; private set; }

        public CntRelationDTO(
            Id id=null,
            TLeftId leftId=null,
            TRightId rightId=null,
            int count=0
        ) : base(id, leftId, rightId)
        {
            this.Count = count;
        }

        public NoIdCntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        GetCntRelationDTO()
        {
            return new NoIdCntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.LeftId,
                this.RightId,
                this.Count
            );
        }

        public override object Clone()
        {
            return new CntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.Id,
                this.LeftId,
                this.RightId,
                this.Count
            );
        }

        public override bool Equals(IDTO dto)
        {
            var cntRelDto = dto
                as CntRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>;
            if (cntRelDto == null)                 return false;
            if (cntRelDto.Count != this.Count) return false;
            return base.Equals(cntRelDto);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int r = base.GetHashCode() * this.Count+1;
                return r;
            }
        }

        protected override W0Set<object> GetRelationCombo()
        {
            var s = base.GetRelationCombo();
            s.Add(this.Count);
            return s;
        }
    }
}