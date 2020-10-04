using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class TagReg
        : IEntityReg<Tag, Name, string>
    {
        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._repo; } }

        public TagReg(ITagRepo tagRepo)
            : base(tagRepo)
        { }
    }
}