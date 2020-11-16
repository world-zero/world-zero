using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// MetaTask is a entity for a tuple of the MetaTask table.
    /// </summary>
    public class MetaTask : IIdEntity, IOptionalEntity
    {
        public MetaTask(
            Name factionId,
            Name statusId,
            string description,
            PointTotal bonus,
            bool isFlatBonus=false
        )
            : base()
        {
            this._setup(
                factionId,
                statusId,
                description,
                bonus,
                isFlatBonus
            );
        }

        public MetaTask(
            Id id,
            Name factionId,
            Name statusId,
            string description,
            PointTotal bonus,
            bool isFlatBonus=false
        )
            : base(id)
        {
            this._setup(
                factionId,
                statusId,
                description,
                bonus,
                isFlatBonus
            );
        }

        internal MetaTask(
            int id,
            string factionId,
            string statusId,
            string description,
            double bonus,
            bool isFlatBonus
        )
            : base(new Id(id))
        {
            this._setup(
                new Name(factionId),
                new Name(statusId),
                description,
                new PointTotal(bonus),
                isFlatBonus
            );
        }

        private void _setup(
            Name factionId,
            Name statusId,
            string description,
            PointTotal bonus,
            bool isFlatBonus
        )
        {
            this.FactionId = factionId;
            this.StatusId = statusId;
            this.Description = description;
            this.Bonus = bonus;
            this.IsFlatBonus = isFlatBonus;
        }

        public override IEntity<Id, int> Clone()
        {
            return new MetaTask(
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

        /// <summary>
        /// Bonus is a PointTotal that can be either a flag point
        /// bonus or a point percentage modifier. For more, see
        /// <see cref="MetaTask.IsFlatBonus"/>.
        /// </summary>
        /// <remarks>
        /// If `IsFlatBonus` is false, then this will act as the percentage
        /// of the point total to add.
        /// </remarks>
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

        /// <summary>
        /// IsFlatBonus determines whether <c>Bonus</c> is a flat bonus point
        /// addition or if it is a point percentage modifier.
        /// </summary>
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

        public Name StatusId
        {
            get { return this._statusId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("StatusId");
                this._statusId = value;
            }
        }
        private Name _statusId;
    }
}