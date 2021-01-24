using System;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Unspecified.Primary
{
    /// <remarks>
    /// Note how Equals(object) casts to an IDTO and then returns the result of
    /// Equals(IDTO) - this will allow children to just override that method
    /// and be set.
    /// </remarks>
    /// <inheritdoc cref="IEntityDTO{TId, TBuiltIn}"/>
    public class EntityDTO<TId, TBuiltIn> : IEntityDTO<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        public virtual TId Id
        {
            get { return this._id; } 
            // This was set is basically private, as desired.
            set { throw new NotSupportedException(); }
        }
        private TId _id;

        public EntityDTO(TId id)
        {
            this._id = id;
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

        public virtual W0List<W0Set<object>> GetUniqueRules()
        {
            return new W0List<W0Set<object>>();
        }

        /// <summary>
        /// Compare the two objects like an XOR operator - if you do not know
        /// what operator that is, go look it up.
        /// </summary>
        /// <param name="left">The expected value.</param>
        /// <param name="right">The actual value.</param>
        /// <returns>
        /// True iff left and right are both null or if left .Equals
        /// right.
        /// </returns>
        /// <remarks>
        /// This is called XOR because normal XOR is used for booleans,
        /// but this is used for nullable objects.
        /// </remarks>
        protected bool XOR(object left, object right)
        {
            if (left == null)
                return right == null;

            return left.Equals(right);
        }
    }
}