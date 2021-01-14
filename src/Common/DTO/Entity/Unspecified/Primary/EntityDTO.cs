using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Unspecified.Primary
{
    /// <inheritdoc cref="IEntityDTO{TId, TBuiltIn}"/>
    public class EntityDTO<TId, TBuiltIn> : IEntityDTO<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        public TId Id { get; private set; }

        public EntityDTO(TId id)
        {
            this.Id = id;
        }

        public virtual object Clone()
        {
            return new EntityDTO<TId, TBuiltIn>(this.Id);
        }

        public override bool Equals(object obj)
        {
            IDTO dto = obj as IDTO;
            return this.Equals(dto);
        }

        public virtual bool Equals(IDTO dto)
        {
            var entityDto = dto as EntityDTO<TId, TBuiltIn>;
            if (entityDto == null) return false;
            if (entityDto.Id != this.Id) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (this.Id == null)
                    return base.GetHashCode();
                else
                    return base.GetHashCode() * this.Id.GetHashCode();
            }
        }
    }
}