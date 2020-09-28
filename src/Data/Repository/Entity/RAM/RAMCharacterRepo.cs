using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="ICharacterRepo"/>
    public class RAMCharacterRepo
        : IRAMIdNamedEntityRepo<Character>,
        ICharacterRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Character(new Name("asdf"), new Id(2));
            return a.GetUniqueRules().Count;
        }
    }
}