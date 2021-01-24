using System;
using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
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

        public UnsafePraxis(IPraxisDTO dto)
            : base(dto.Id, dto.StatusId)
        {
            this._setup(
                dto.TaskId,
                dto.Points,
                dto.MetaTaskId,
                dto.AreDueling
            );
        }

        public override object Clone()
        {
            return new PraxisDTO(
                this.Id,
                this.StatusId,
                this.TaskId,
                this.Points,
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