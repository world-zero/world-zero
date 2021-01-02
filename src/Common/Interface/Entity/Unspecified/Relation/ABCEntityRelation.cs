using System;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class ABCEntityRelation
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : ABCIdEntity,
          IEntityRelation<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
    {
        // NOTE: IEntity.Clone() is still not implemmented.

        public ABCEntityRelation(TLeftId leftId, TRightId rightId)
            : base()
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public ABCEntityRelation(Id id, TLeftId leftId, TRightId rightId)
            : base(id)
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public TLeftId LeftId
        {
            get { return this._leftId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("LeftId");
                this._leftId = value;
            }
        }
        protected TLeftId _leftId;

        public TRightId RightId
        {
            get { return this._rightId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("RightId");
                this._rightId = value;
            }
        }
        protected TRightId _rightId;

        public abstract RelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        GetDTO();

        public override bool Equals(object obj)
        {
            if ( (obj == null) || (obj.GetType() != this.GetType()) )
                return false;

            ABCEntityRelation
            <
                TLeftId,
                TLeftBuiltIn,
                TRightId,
                TRightBuiltIn
            > other;
            try
            {
                other = (ABCEntityRelation
                <
                    TLeftId,
                    TLeftBuiltIn,
                    TRightId,
                    TRightBuiltIn
                >) obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }

            if ( (this.LeftId == other.LeftId)
              && (this.RightId == other.RightId) )
            {
                return true;
            }
            else if ( (this.LeftId == other.RightId)
                   && (this.RightId == other.LeftId) )
            {
                return true;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.LeftId.Get.GetHashCode()
                 * this.RightId.Get.GetHashCode();
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
