using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="IPlayerRepo"/>
    public class RAMPlayerRepo
        : IRAMIdNamedEntityRepo<Player>,
        IPlayerRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Player(new Name("Hal"));
            return a.GetUniqueRules().Count;
        }
    }
}