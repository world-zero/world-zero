using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Praxis is a entity for a tuple of the Praxis table.
    /// </summary>
    /// <remarks>
    /// As you would expect, validation of the Status is left to the Praxis
    /// registration class.
    /// </remarks>
    public class Praxis : IIdStatusedEntity, IEntityHasOptional
    {
        public Praxis(
            Id taskId,
            PointTotal points,
            Name statusId,
            Id metaTaskId=null,
            bool areDueling=false
        )
            : base(statusId)
        {
            this._setup(
                taskId,
                points,
                metaTaskId,
                areDueling
            );
        }

        public Praxis(
            Id id,
            Id taskId,
            PointTotal points,
            Name statusId,
            Id metaTaskId=null,
            bool areDueling=false
        )
            : base(id, statusId)
        {
            this._setup(
                taskId,
                points,
                metaTaskId,
                areDueling
            );
        }

        internal Praxis(
            int id,
            int taskId,
            int points,
            string statusId,
            int metaTaskId,
            bool areDueling
        )
            : base(new Id(id), new Name(statusId))
        {
            this._setup(
                new Id(taskId),
                new PointTotal(points),
                new Id(metaTaskId),
                areDueling
            );
        }

        public override IEntity<Id, int> Clone()
        {
            return new Praxis(
                this.Id,
                this.TaskId,
                this.Points,
                this.StatusId,
                this.MetaTaskId,
                this.AreDueling
            );
        }

        private void _setup(
            Id taskId,
            PointTotal points,
            Id metaTaskId,
            bool areDueling
        )
        {
            if (taskId == null)
                throw new ArgumentNullException("taskId");
            if (points == null)
                throw new ArgumentNullException("points");
            this.TaskId = taskId;
            this.Points = points;
            this.MetaTaskId = metaTaskId;
            this.AreDueling = areDueling;
        }

        public Id TaskId
        {
            get { return this._taskId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("TaskId");
                this._taskId = value;
            }
        }
        private Id _taskId;

        public PointTotal Points
        {
            get { return this._points; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Points");

                this._points = value;
            }
        }
        private PointTotal _points;

        public Id MetaTaskId { get; set; }

        /// <summary>
        /// This field will control whether or the the two participants are
        /// dueling. A duel requires two participants, and as the praxis is
        /// moved from being active to being retired, the person with the most
        /// points earned via Votes will get the points of the Praxis instead
        /// of both participants, had they been collaberating.
        /// </summary>
        public bool AreDueling { get; set; }
    }
}