using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// MetaTask is a entity for a tuple of the MetaTask table.
    /// </summary>
    public class UnsafeMetaTask :
        ABCIdStatusedEntity,
        IOptionalEntity,
        IUnsafeEntity
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

        public override ABCEntity<Id, int> Clone()
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

        /// <summary>
        /// Bonus is a PointTotal that can be either a flag point
        /// bonus or a point percentage modifier. For more, see
        /// <see cref="UnsafeMetaTask.IsFlatBonus"/>.
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
    }
}