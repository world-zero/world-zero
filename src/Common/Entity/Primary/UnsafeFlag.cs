using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IFlag"/>
    public class UnsafeFlag : ABCNamedEntity, IFlag
    {
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

        public override IEntity<Name, string> CloneAsEntity()
        { 
            return new UnsafeFlag(
                this.Id
            );
        }

        public string Description { get; set; }

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

        public bool IsFlatPenalty { get; set; }
    }
}
