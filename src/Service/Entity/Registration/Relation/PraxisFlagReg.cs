using System;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    public class PraxisFlagReg
        : IEntityRelationReg
        <
            UnsafePraxisFlag,
            UnsafePraxis,
            Id,
            int,
            UnsafeFlag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IUnsafePraxisFlagRepo _praxisFlagRepo
        { get { return (IUnsafePraxisFlagRepo) this._repo; } }

        protected IUnsafePraxisRepo _praxisRepo
        { get { return (IUnsafePraxisRepo) this._leftRepo; } }

        protected IUnsafeFlagRepo _flagRepo
        { get { return (IUnsafeFlagRepo) this._rightRepo; } }

        public PraxisFlagReg(
            IUnsafePraxisFlagRepo praxisFlagRepo,
            IUnsafePraxisRepo praxisRepo,
            IUnsafeFlagRepo flagRepo
        )
            : base(praxisFlagRepo, praxisRepo, flagRepo)
        { }

        public override UnsafePraxisFlag Register(UnsafePraxisFlag e)
        {
            // NOTE: This code exists in PraxisFlagReg.Register(),
            // MetaPraxisFlagReg.Register(), and PraxisFlagReg.Register()
            // because of the weird non-generic abstract classes that exist
            // right before the entities are implemented.
            this.AssertNotNull(e, "e");
            UnsafePraxis p;
            UnsafeFlag f;
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

            p.Points = PointTotal
                .ApplyPenalty(p.Points, f.Penalty, f.IsFlatPenalty);

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