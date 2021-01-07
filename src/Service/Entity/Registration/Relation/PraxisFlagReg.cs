using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Service.Interface.Entity.Update.Primary;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IPraxisFlagReg"/>
    public class PraxisFlagReg
        : ABCEntityRelationReg
        <
            IPraxisFlag,
            IPraxis,
            Id,
            int,
            IFlag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >, IPraxisFlagReg
    {
        protected IPraxisFlagRepo _praxisFlagRepo
        { get { return (IPraxisFlagRepo) this._repo; } }

        protected IPraxisRepo _praxisRepo
        { get { return (IPraxisRepo) this._leftRepo; } }

        protected readonly IPraxisUpdate _praxisUpdate;

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public PraxisFlagReg(
            IPraxisFlagRepo praxisFlagRepo,
            IPraxisRepo praxisRepo,
            IPraxisUpdate praxisUpdate,
            IFlagRepo flagRepo
        )
            : base(praxisFlagRepo, praxisRepo, flagRepo)
        {
            this.AssertNotNull(praxisUpdate, "praxisUpdate");
            this._praxisUpdate = praxisUpdate;
        }

        public override IPraxisFlag Register(IPraxisFlag e)
        {
            // NOTE: This code exists in PraxisFlagReg.Register(),
            // MetaPraxisFlagReg.Register(), and PraxisFlagReg.Register()
            // because of the weird non-generic abstract classes that exist
            // right before the entities are implemented.
            this.AssertNotNull(e, "e");
            IPraxis p;
            IFlag f;
            this._praxisFlagRepo.BeginTransaction(true);
            try
            {
                p = this.GetLeftEntity(e);
                f = this.GetRightEntity(e);
            } catch (ArgumentException exc)
            {
                this._praxisFlagRepo.DiscardTransaction();
                throw new ArgumentException("Could not retrieve an associated entity.", exc);
            }

            this._praxisUpdate.AmendPoints(
                p,
                PointTotal.ApplyPenalty(p.Points, f.Penalty, f.IsFlatPenalty)
            );

            try
            {
                this._praxisRepo.Update(p);
                this._praxisFlagRepo.Insert(e);
                this._praxisFlagRepo.EndTransaction();
                return e;
            }
            catch (ArgumentException exc)
            {
                this._praxisFlagRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }
    }
}