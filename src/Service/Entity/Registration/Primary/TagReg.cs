using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class TagReg
        : IEntityReg<UnsafeTag, Name, string>
    {
        protected IUnsafeTagRepo _tagRepo
        { get { return (IUnsafeTagRepo) this._repo; } }

        public TagReg(IUnsafeTagRepo tagRepo)
            : base(tagRepo)
        { }
    }
}