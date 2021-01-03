using System;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <inheritdoc cref="IIdEntity"/>
    /// <summary>
    /// This derivation contains a `StatusId`, which is a name denoting the
    /// concrete entity's status.
    /// </summary>
    public abstract class IIdStatusedEntity : IIdEntity
    {
        public IIdStatusedEntity(Name statusId)
            : base()
        {
            this.StatusId = statusId;
        }

        public IIdStatusedEntity(Id id, Name statusId)
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