using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="ITagReg"/>
    public class TagReg
        : ABCEntityReg<ITag, Name, string>, ITagReg
    {
        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._repo; } }

        public TagReg(ITagRepo tagRepo)
            : base(tagRepo)
        { }
    }
}