using System;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Unspecified.Primary
{
    public abstract class ABCIdNamedEntity : ABCIdEntity, IIdNamedEntity
    {
        public Name Name
        {
            get { return this._name; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Name");

                this._name = value;
            }
        }
        private Name _name;

        public ABCIdNamedEntity(Name name)
            : base()
        {
            this.Name = name;
        }

        public ABCIdNamedEntity(Id id, Name name)
            : base(id)
        {
            this.Name = name;
        }
    }
}
