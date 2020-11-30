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
    public class Praxis : IIdEntity, IEntityHasOptional
    {
        public Praxis(
            Id taskId,
            PointTotal points,
            Name statusId,
            Id metaTaskId=null,
            bool areDueling=false
        )
            : base()
        {
            this._setup(
                taskId,
                points,
                statusId,
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
            : base(id)
        {
            this._setup(
                taskId,
                points,
                statusId,
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
            : base(new Id(id))
        {
            this._setup(
                new Id(taskId),
                new PointTotal(points),
                new Name(statusId),
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
            Name statusId,
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
            this.StatusId = statusId;
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

        public Id MetaTaskId { get; set; }
        public bool AreDueling { get; set; }
    }
}