using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public interface IStatusReg
        : IEntityReg<IStatus, Name, string>
    {
        static readonly UnsafeStatus Proposed =
            new UnsafeStatus(new Name("Proposed"));
        static readonly UnsafeStatus Active =
            new UnsafeStatus(new Name("Active"));
        static readonly UnsafeStatus InProgress =
            new UnsafeStatus(new Name("In Progress"));
        static readonly UnsafeStatus Retired =
            new UnsafeStatus(new Name("Retired"));
    }
}