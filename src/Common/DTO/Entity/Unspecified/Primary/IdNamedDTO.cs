using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Unspecified.Primary
{
    /// <inheritdoc cref="IEntityDTO{TId, TBuiltIn}"/>
    public class IdNamedDTO : EntityDTO<Id, int>, IIdNamedDTO
    {
        public Name Name { get; private set; }

        public IdNamedDTO(Id id, Name name)
            : base(id)
        {
            this.Name = name;
        }

        public override object Clone()
        {
            return new IdNamedDTO(this.Id, this.Name);
        }

        public override bool Equals(IDTO dto)
        {
            var idNamed = dto as IdNamedDTO;
            if (idNamed == null)           return false;
            if (idNamed.Name != this.Name) return false;
            return base.Equals(idNamed);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (this.Name == null)
                    return base.GetHashCode();
                else
                    return base.GetHashCode() * this.Name.GetHashCode();
            }
        }

        public override W0List<W0Set<object>> GetUniqueRules()
        {
            var r = base.GetUniqueRules();
            var n = new W0Set<object>();
            n.Add(this.Name);
            r.Add(n);
            return r;
        }
    }
}