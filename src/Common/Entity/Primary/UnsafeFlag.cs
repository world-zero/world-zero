using System;
using WorldZero.Common.Interface.DTO.Entity.Primary;
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
            this._setup(description, penalty, isFlatPenalty);
        }

        public UnsafeFlag(IFlagDTO dto)
            : base(dto.Id)
        {
            this._setup(
                dto.Description,
                dto.Penalty,
                dto.IsFlatPenalty
            );
        }

        private void _setup(
            string desc,
            PointTotal penalty,
            bool isFlatPenalty
        )
        {
            this.Description = desc;
            if (penalty == null)
                penalty = new PointTotal(0.1);
            this.Penalty = penalty;
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
