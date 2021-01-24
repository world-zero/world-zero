using System;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Unspecified.Primary
{
    /// <remarks>
    /// Naturally, the equality of two entities is determined by comparing
    /// their IDs.
    /// <br />
    /// This ABC will take care of Equals(IDTO) (to compare the IDs) and the
    /// GetUniqueRules() (by cloning the entity and casting that DTO to
    /// IEntityDTO and returning those rules). This inherits Equals(object) and
    /// GetHashCode() from EntityDTO. The only things children must implement
    /// are:
    /// <br />
    /// a) Clone (be sure to return a clone of the DTO, not the entity).
    /// Naturally, only concrete children will be implementing this method.
    /// <br />
    /// b) Equals(object), Equals(IDTO), and GetHashCode() if any of these need
    /// adjustments.
    /// </remarks>
    public abstract class ABCEntity<TId, TBuiltIn> :
        EntityDTO<TId, TBuiltIn>,
        IEntity<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        public bool IsIdSet()
        {
            if (this._id == null)
                return false;
            return !this._id.Equals(this.UnsetIdValue);
        }

        public override bool Equals(IDTO dto)
        {
            var e = dto as IEntity<TId, TBuiltIn>;
            if (e == null) return false;
            if (e.Id != this.Id) return false;
            return true;
        }

        public override W0List<W0Set<object>> GetUniqueRules()
        {
            // I feel very smart about this.
            return ((IEntityDTO<TId, TBuiltIn>) this.Clone()).GetUniqueRules();
        }

        public override TId Id
        {
            get { return this._id; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Id");

                if (this.IsIdSet())
                    throw new ArgumentException("Attempted to re-set the `Id` of an entity.");

                this._id = value;
            }
        }
        private TId _id;

        /// <summary>
        /// An ID with this value is considered unset, and can still be
        /// changed.
        /// </summary>
        protected readonly TId UnsetIdValue;

        public ABCEntity(TId unsetValue)
            : base(null)
        {
            this.UnsetIdValue = unsetValue;
            this._id = this.UnsetIdValue;
        }
    }
}