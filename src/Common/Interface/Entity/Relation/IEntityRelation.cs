using System;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <summary>
    /// This entity represents a many-to-many mapping between two entities by
    /// containing their IDs. Equality is determined by the keys of the two
    /// entities. If these keys are the same type, then either order will be
    /// considered the same. If the keys are of a different type, then order
    /// will be considered, meaning a child with an `Id` LeftId and a `Name`
    /// RightId will not be considered equal to a child with a `Name` LeftId
    /// and an `Id` RightId, even if the corresponding properties have the same
    /// values.
    /// Additionally, validatation of IDs falls to the repository and service
    /// class(es), as necessary.
    /// </summary>
    /// <remarks>
    /// As usual, enforcing that the combination of the left and right is the
    /// responsiblity of the repo.  
    /// When making an implementation, please have the class name match the
    /// order of the enitites (ie: an implementation that maps left entity
    /// `Praxis` and right entity `Flag` should be named something like
    /// `PraxisFlag`). An exception to this rule is for self-referencial
    /// relations.
    /// It is worth noting that == and != are not overloaded, just .Equals().
    /// It is recommended that children add more descriptive wrapper properties
    /// for LeftId and RightId.
    /// </remarks>
    public abstract class IEntityRelation
        <TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>
        : IIdEntity
        where TLeftSVO  : ISingleValueObject<TLeftBuiltIn>
        where TRightSVO : ISingleValueObject<TRightBuiltIn>
    {
        // NOTE: IEntity.DeepCopy() is still not implemmented.

        public IEntityRelation(TLeftSVO leftId, TRightSVO rightId)
            : base()
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public IEntityRelation(Id id, TLeftSVO leftId, TRightSVO rightId)
            : base(id)
        {
            this.LeftId = leftId;
            this.RightId = rightId;
        }

        public TLeftSVO LeftId
        {
            get { return this._leftId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("LeftId");
                this._leftId = value;
            }
        }
        protected TLeftSVO _leftId;

        public TRightSVO RightId
        {
            get { return this._rightId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("RightId");
                this._rightId = value;
            }
        }
        protected TRightSVO _rightId;

        public abstract IDualDTO
        <TLeftSVO, TLeftBuiltIn, TRightSVO, TRightBuiltIn>
        GetDTO();

        public override bool Equals(object obj)
        {
            if ( (obj == null) || (obj.GetType() != this.GetType()) )
                return false;

            IEntityRelation
            <
                TLeftSVO,
                TLeftBuiltIn,
                TRightSVO,
                TRightBuiltIn
            > other;
            try
            {
                other = (IEntityRelation
                <
                    TLeftSVO,
                    TLeftBuiltIn,
                    TRightSVO,
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
    }
}