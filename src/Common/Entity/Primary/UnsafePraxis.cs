using System;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IPraxis"/>
    public class UnsafePraxis :
        ABCIdStatusedEntity,
        IPraxis
    {
        public UnsafePraxis(
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

        public UnsafePraxis(
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

        internal UnsafePraxis(
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

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafePraxis(
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

        public bool AreDueling { get; set; }
    }
}