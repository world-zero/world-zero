using System;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Flag is a entity for a tuple of the Flag table.
    /// </summary>
    public class UnsafeFlag : UnsafeINamedEntity, IUnsafeEntity
    {
        // This does not have a description parameter since that would make the
        // two constructors ambiguous.
        public UnsafeFlag(
            Name name,
            string description=null,
            PointTotal penalty=null,
            bool isFlatPenalty=false
        )
            : base(name)
        {
            this.Description = description;
            if (penalty == null)
                penalty = new PointTotal(0.1);
            this.Penalty = penalty;
            this.IsFlatPenalty = isFlatPenalty;
        }

        internal UnsafeFlag(
            string name,
            string description,
            double penalty,
            bool isFlatPenalty
        )
            : base(new Name(name))
        {
            this.Description = description;
            this.Penalty = new PointTotal(penalty);
            this.IsFlatPenalty = isFlatPenalty;
        }

        public override IEntity<Name, string> Clone()
        { 
            return new UnsafeFlag(
                this.Id
            );
        }

        /// <summary>
        /// Description is a description of the tag. This is optional.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Penalty is a PointTotal that can be either a flag point
        /// penalty or a point percentage modifier. For more, see
        /// <see cref="UnsafeFlag.IsFlatPenalty"/>.
        /// </summary>
        /// <remarks>
        /// If `IsFlatPenalty` is false, then this will act as the percentage
        /// of the point total to deduct.
        /// </remarks>
        public PointTotal Penalty
        {
            get { return this._penalty; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Penalty");
                this._penalty = value;
            }
        }
        private PointTotal _penalty;

        /// <summary>
        /// IsFlatPenalty determines whether <c>Penalty</c> is a flat penalty point
        /// addition or if it is a point percentage modifier.
        /// </summary>
        public bool IsFlatPenalty { get; set; }
    }
}
