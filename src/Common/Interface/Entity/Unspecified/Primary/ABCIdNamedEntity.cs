using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Collections.Generic;

namespace WorldZero.Common.Interface.Entity.Unspecified.Primary
{
    public abstract class ABCIdNamedEntity : ABCIdEntity,IIdNamedEntity
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

        public override W0List<W0Set<object>> GetUniqueRules()
        {
            var r = base.GetUniqueRules();
            var n = new W0Set<object>();
            n.Add(this.Name);
            r.Add(n);
            return r;
        }
    }
}
