using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class FactionRegistration
        : IEntityRegistration<Faction, Name, string>
    {
        protected IFactionRepo _factionRepo
        { get { return (IFactionRepo) this._repo; } }

        public FactionRegistration(IFactionRepo factionRepo)
            : base(factionRepo)
        { }
    }
}