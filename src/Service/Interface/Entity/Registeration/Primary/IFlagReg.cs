using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public interface IFlagReg
        : IEntityReg<IFlag, Name, string>
    {
        static readonly UnsafeFlag Duplicate =
            new UnsafeFlag(new Name("Duplicate"));
        static readonly UnsafeFlag Dangerous =
            new UnsafeFlag(new Name("Dangerous"));
        static readonly UnsafeFlag Inappropriate =
            new UnsafeFlag(new Name("Inappropriate"));
    }
}