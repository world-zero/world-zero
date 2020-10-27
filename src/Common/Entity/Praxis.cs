using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Praxis is a entity for a tuple of the Praxis table.
    /// </summary>
    /// <remarks>
    /// As you would expect, validation of the Status is left to the Praxis
    /// registration class.
    /// </remarks>
    public class Praxis : IIdEntity
    {
        public Praxis(
            Id taskId,
            Name statusId,
            Id metaTaskId=null,
            bool areDueling=false
        )
            : base()
        {
            this._setup(
                taskId,
                statusId,
                metaTaskId,
                areDueling
            );
        }

        public Praxis(
            Id id,
            Id taskId,
            Name statusId,
            Id metaTaskId=null,
            bool areDueling=false
        )
            : base(id)
        {
            this._setup(
                taskId,
                statusId,
                metaTaskId,
                areDueling
            );
        }

        internal Praxis(
            int id,
            int taskId,
            string statusId,
            int metaTaskId,
            bool areDueling
        )
            : base(new Id(id))
        {
            this._setup(
                new Id(taskId),
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
                this.StatusId,
                this.MetaTaskId,
                this.AreDueling
            );
        }

        private void _setup(Id taskId, Name statusId, Id metaTaskId, bool areDueling)
        {
            this.TaskId = taskId;
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