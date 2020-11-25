using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;

namespace WorldZero.Data.Repository.RAM.Entity
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