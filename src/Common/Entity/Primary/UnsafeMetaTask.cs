using System;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IMetaTask"/>
    public class UnsafeMetaTask : ABCIdStatusedEntity, IMetaTask
    {
        public UnsafeMetaTask(
            Name factionId,
            Name statusId,
            string description,
            PointTotal bonus,
            bool isFlatBonus=false
        )
            : base(statusId)
        {
            this._setup(
                factionId,
                description,
                bonus,
                isFlatBonus
            );
        }

        public UnsafeMetaTask(
            Id id,
            Name factionId,
            Name statusId,
            string description,
            PointTotal bonus,
            bool isFlatBonus=false
        )
            : base(id, statusId)
        {
            this._setup(
                factionId,
                description,
                bonus,
                isFlatBonus
            );
        }

        internal UnsafeMetaTask(
            int id,
            string factionId,
            string statusId,
            string description,
            double bonus,
            bool isFlatBonus
        )
            : base(new Id(id), new Name(statusId))
        {
            this._setup(
                new Name(factionId),
                description,
                new PointTotal(bonus),
                isFlatBonus
            );
        }

        private void _setup(
            Name factionId,
            string description,
            PointTotal bonus,
            bool isFlatBonus
        )
        {
            this.FactionId = factionId;
            this.Description = description;
            this.Bonus = bonus;
            this.IsFlatBonus = isFlatBonus;
        }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafeMetaTask(
                this.Id,
                this.FactionId,
                this.StatusId,
                this.Description,
                this.Bonus,
                this.IsFlatBonus
            );
        }

        public string Description
        {
            get { return this._description; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Attempted to set a `Description` to null or whitespace.");
                this._description = value;
            }
        }
        private string _description;

        public PointTotal Bonus
        {
            get { return this._bonus; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Bonus");
                this._bonus = value;
            }
        }
        private PointTotal _bonus;

        public bool IsFlatBonus { get; set; }

        public Name FactionId
        {
            get { return this._factionId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("FactionId");
                this._factionId = value;
            }
        }
        private Name _factionId;
    }
}