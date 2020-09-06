using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class TagRegistration
        : IEntityRegistration<Tag, Name, string>
    {
        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._repo; } }

        public TagRegistration(ITagRepo tagRepo)
            : base(tagRepo)
        { }
    }
}