using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class TagReg
        : ABCEntityReg<UnsafeTag, Name, string>
    {
        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._repo; } }

        public TagReg(ITagRepo tagRepo)
            : base(tagRepo)
        { }
    }
}