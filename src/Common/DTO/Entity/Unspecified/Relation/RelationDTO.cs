using WorldZero.Common.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Relation;

namespace WorldZero.Common.DTO.Entity.Unspecified.Relation
{
    public class RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : EntityDTO<Id, int>,
        IEntityRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
        public TLeftId LeftId { get; private set; }
        public TRightId RightId { get; private set; }

        public RelationDTO(
            Id id=null,
            TLeftId leftId=null,
            TRightId rightId=null
        ) : base(id)
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        GetNoIdRelationDTO()
        {
            return new NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.LeftId,
                this.RightId
            );
        }

        public override object Clone()
        {
            return new RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var relDto = dto
                as RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>;
            if (relDto == null)                 return false;
            if (relDto.LeftId != this.LeftId)   return false;
            if (relDto.RightId != this.RightId) return false;
            return base.Equals(relDto);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int r = base.GetHashCode();
                if (this.Id != null)
                    r += this.Id.GetHashCode();
                if (this.LeftId != null)
                    r *= this.LeftId.GetHashCode();
                if (this.RightId != null)
                    r -= this.RightId.GetHashCode();
                return r;
            }
        }

        public override W0List<W0Set<object>> GetUniqueRules()
        {
            var r = base.GetUniqueRules();
            r.Add(this.GetRelationCombo());
            return r;
       }

        protected virtual W0Set<object> GetRelationCombo()
        {
            var s = new W0Set<object>();
            s.Add(this.LeftId);
            s.Add(this.RightId);
            return s;
        }
    }
}