using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;

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
        public Praxis(Id taskId, Name statusId, bool areDueling=false)
            : base()
        {
            this._setup(
                taskId,
                statusId,
                areDueling
            );
        }

        public Praxis(Id id, Id taskId, Name statusId, bool areDueling=false)
            : base(id)
        {
            this._setup(
                taskId,
                statusId,
                areDueling
            );
        }

        internal Praxis(int id, int taskId, string statusId, bool areDueling)
            : base(new Id(id))
        {
            this._setup(
                new Id(taskId),
                new Name(statusId),
                areDueling
            );
        }

        public override IEntity<Id, int> DeepCopy()
        {
            return new Praxis(
                this.Id,
                this.TaskId,
                this.StatusId,
                this.AreDueling
            );
        }

        private void _setup(Id taskId, Name statusId, bool areDueling)
        {
            this.TaskId = taskId;
            this.StatusId = statusId;
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

        public bool AreDueling { get; set; }

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