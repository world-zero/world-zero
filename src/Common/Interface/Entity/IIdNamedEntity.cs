using WorldZero.Common.ValueObject.General;
using System;

namespace WorldZero.Common.Interface.Entity
{
    /// <inheritdoc cref="IIdEntity">
    /// <summary>
    /// This class exists to have entities that have an `Id` ID property, and
    /// also have a required name property - critically, this name must be
    /// unique. As with this type of rule, uniqueness should be enforced by the
    /// repo. This name can only be valid.
    /// </summary>
    public abstract class IIdNamedEntity : IIdEntity
    {
        /// <summary>
        /// This is a `Name` that is not an ID of an entity, but rather is a
        /// `Name` that a repo must enforce to be unique. It is safe to assume
        /// that an `IIdNamedEntity` will always contain a valid name.
        /// </summary>
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

        public IIdNamedEntity(Name name)
            : base()
        {
            this.Name = name;
        }

        public IIdNamedEntity(Id id, Name name)
            : base(id)
        {
            this.Name = name;
        }
    }
}
