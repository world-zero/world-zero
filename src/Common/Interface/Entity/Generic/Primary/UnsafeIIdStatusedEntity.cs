using System;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class UnsafeIIdStatusedEntity : UnsafeIIdEntity, IIdEntity
    {
        public UnsafeIIdStatusedEntity(Name statusId)
            : base()
        {
            this.StatusId = statusId;
        }

        public UnsafeIIdStatusedEntity(Id id, Name statusId)
            : base(id)
        {
            this.StatusId = statusId;
        }

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