using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Collections.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class UnsafeIIdNamedEntity : UnsafeIIdEntity,IIdNamedEntity
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

        public UnsafeIIdNamedEntity(Name name)
            : base()
        {
            this.Name = name;
        }

        public UnsafeIIdNamedEntity(Id id, Name name)
            : base(id)
        {
            this.Name = name;
        }

        internal override W0List<W0Set<object>> GetUniqueRules()
        {
            var r = base.GetUniqueRules();
            var n = new W0Set<object>();
            n.Add(this.Name);
            r.Add(n);
            return r;
        }
    }
}
