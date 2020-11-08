using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Flag is a entity for a tuple of the Flag table.
    /// </summary>
    public class Flag : INamedEntity
    {
        // This does not have a description parameter since that would make the
        // two constructors ambiguous.
        public Flag(
            Name name,
            string description=null,
            double penalty=0.1,
            bool isFlatPenalty=false
        )
            : base(name)
        {
            this.Description = description;
            this.Penalty = penalty;
            this.IsFlatPenalty = isFlatPenalty;
        }

        internal Flag(
            string name,
            string description,
            double penalty,
            bool isFlatPenalty
        )
            : base(new Name(name))
        {
            this.Description = description;
            this.Penalty = penalty;
            this.IsFlatPenalty = isFlatPenalty;
        }

        public override IEntity<Name, string> Clone()
        { 
            return new Flag(
                this.Id
            );
        }

        /// <summary>
        /// Description is a description of the tag. This is optional.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Penalty is a non-negative double that can be either a flag point
        /// penalty or a point percentage modifier. For more, see
        /// <see cref="Flag.IsFlatPenalty"/>.
        /// </summary>
        /// <remarks>
        /// If `IsFlatPenalty` is false, then this will act as the percentage
        /// of the point total to deduct.
        /// </remarks>
        public double Penalty
        {
            get { return this._penalty; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("A penalty must be a positive number.");
                this._penalty = value;
            }
        }
        private double _penalty;

        /// <summary>
        /// IsFlatPenalty determines whether <c>Penalty</c> is a flat penalty point
        /// addition or if it is a point percentage modifier.
        /// </summary>
        public bool IsFlatPenalty { get; set; }
    }
}
