using System;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntityRelation"/>
    public abstract class ABCEntityRelation
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        : ABCIdEntity,
          IEntityRelation<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
    {
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

        public abstract NoIdRelationDTO
        <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        GetNoIdRelationDTO();

        public override bool Equals(IDTO dto)
            => this.Equals((object) dto);

        public override bool Equals(object obj)
        {
            var other = obj as ABCEntityRelation
                <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>;
            if (other == null)
                return false;

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
    }
}
