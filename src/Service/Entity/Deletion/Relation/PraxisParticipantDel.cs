using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.General.Generic;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Primary;
using WorldZero.Service.Interface.Entity.Deletion.Relation;
using WorldZero.Service.Interface.Entity.Update.Primary;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("WorldZero.Test.Integration")]

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IPraxisParticipantDel"/>
    public class PraxisParticipantDel : ABCEntityRelationDel
    <
        IPraxisParticipant,
        IPraxis,
        Id,
        int,
        ICharacter,
        Id,
        int,
        NoIdRelationDTO<Id, int, Id, int>
    >, IPraxisParticipantDel
    {
        protected IPraxisParticipantRepo _ppRepo
        { get { return (IPraxisParticipantRepo) this._relRepo; } }

        protected readonly IPraxisRepo _praxisRepo;
        protected readonly IPraxisUpdate _praxisUpdate;
        protected readonly VoteDel _voteDel;

        public PraxisParticipantDel(
            IPraxisParticipantRepo repo,
            IPraxisRepo praxisRepo,
            IPraxisUpdate praxisUpdate,
            VoteDel voteDel
        )
            : base(repo)
        {
            this.AssertNotNull(praxisRepo, "praxisRepo");
            this.AssertNotNull(praxisUpdate, "praxisUpdate");
            this.AssertNotNull(voteDel, "voteDel");

            this._praxisRepo = praxisRepo;
            this._praxisUpdate = praxisUpdate;
            this._voteDel = voteDel;
        }

        /// <summary>
        /// WARNING
        /// <br />
        /// As the name implies, this method is an unsafe version of
        /// `DeleteByPraxis` - this is because it will not actually `Delete()`
        /// all participants on the praxis, which is generally not allowed.
        /// </summary>
        /// <remarks>
        /// This should really only be used by <see
        /// cref="WorldZero.Service.Entity.Deletion.Primary.PraxisDel"/> - use
        /// this anywhere else with extreme caution.
        /// </remarks>
        internal void UNSAFE_DeleteByPraxis(IPraxis p)
        {
            this.AssertNotNull(p, "p");
            this.UNSAFE_DeleteByPraxis(p.Id);
        }

        /// <summary>
        /// WARNING
        /// <br />
        /// As the name implies, this method is an unsafe version of
        /// `DeleteByPraxis` - this is because it will not actually `Delete()`
        /// all participants on the praxis, which is generally not allowed.
        /// </summary>
        /// <remarks>
        /// This should really only be used by <see
        /// cref="WorldZero.Service.Entity.Deletion.Primary.PraxisDel"/> - use
        /// this anywhere else with extreme caution.
        /// </remarks>
        internal void UNSAFE_DeleteByPraxis(Id praxisId)
        {
            void f(Id id)
            {
                IEnumerable<IPraxisParticipant> pps;
                try
                {
                    pps = this._ppRepo.GetByPraxisId(id);
                    foreach (IPraxisParticipant pp in pps)
                        this._delete(pp.Id, false);
                }
                catch (ArgumentException)
                { return; }
            }
            this.Transaction<Id>(f, praxisId, true);
        }

        public void DeleteByPraxis(IPraxis p)
        {
            this.DeleteByLeft(p);
        }

        public void DeleteByPraxis(Id id)
        {
            this.DeleteByLeft(id);
        }

        public async Task DeleteByPraxisAsync(IPraxis p)
        {
            this.AssertNotNull(p, "p");
            await Task.Run(() => this.DeleteByPraxis(p));
        }

        public async Task DeleteByPraxisAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByPraxis(id));
        }

        public void DeleteByCharacter(ICharacter c)
        {
            this.DeleteByRight(c);
        }

        public void DeleteByCharacter(Id id)
        {
            this.DeleteByRight(id);
        }

        public async Task DeleteByCharacterAsync(ICharacter c)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.DeleteByCharacter(c));
        }

        public async Task DeleteByCharacterAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByCharacter(id));
        }

        public void SudoDeleteByCharacter(ICharacter c, IPraxisDel praxisDel)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(praxisDel, "praxisDel");
            this.SudoDeleteByCharacter(c.Id, praxisDel);
        }

        public void SudoDeleteByCharacter(Id charId, IPraxisDel praxisDel)
        {
            this.AssertNotNull(praxisDel, "praxisDel");
            void f(Id id0) => this._deleteByChar(id0, praxisDel);
            this.Transaction<Id>(f, charId, true);
        }

        public async
        Task SudoDeleteByCharacterAsync(
            ICharacter c,
            IPraxisDel praxisDel
        )
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(praxisDel, "praxisDel");
            await Task.Run(() => this.SudoDeleteByCharacter(c, praxisDel));
        }

        public async Task SudoDeleteByCharacterAsync(
            Id id,
            IPraxisDel praxisDel
        )
        {
            this.AssertNotNull(id, "id");
            this.AssertNotNull(praxisDel, "praxisDel");
            await Task.Run(() => this.SudoDeleteByCharacter(id, praxisDel));
        }

        /// <summary>
        /// WARNING
        /// <br />
        /// Do not supply this with pDel unless the callee explicitally needs
        /// it.
        /// </summary>
        private void _deleteByChar(Id charId, IPraxisDel pDel)
        {
            var dtos = this._ppRepo.GetParticipantCountsViaCharId(charId);
            foreach (CountingDTO<Id> dto in dtos)
            {
                int endCount = dto.Count - 1;
                if (endCount < 1)
                {
                    if (pDel == null)
                        throw new ArgumentException($"Could not finish deletion, it would leave no participants on praxis {dto.Countee.Get}.");
                    pDel.Delete(dto.Countee);
                }
                else if (endCount == 1)
                    this._unduel(dto.Countee);
            }
            base.DeleteByRight(charId);
        }

        /// <summary>
        /// Change a praxis' AreDueling property to False. Only use this within
        /// a serialized transaction.
        /// </summary>
        private void _unduel(Id praxisId)
        {
            IPraxis p;
            try
            {
                p = this._praxisRepo.GetById(praxisId);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("Could not un-duel a praxis, it is unclear how the supplied praxis ID is supposed to exist.", e); }

            if (p.AreDueling)
                this._praxisUpdate.AmendAreDueling(p, false);
        }

        /// <summary>
        /// WARNING
        /// <br />
        /// Do not use `safeMode=false` unless you are `UNSAFE_DeleteByPraxis`.
        /// For more, see that method.
        /// </summary>
        private void _delete(Id ppId, bool safeMode=true)
        {
            int endCount =
                this._ppRepo.GetParticipantCountViaPPId(ppId) - 1;
            if (endCount < 1)
            {
                if (safeMode)
                    throw new ArgumentException($"Could not finish deletion, it would leave no participants on praxis {ppId.Get}.");
            }

            if (endCount == 1)
            {
                IPraxisParticipant pp;
                try
                { pp = this._ppRepo.GetById(ppId); }
                catch (ArgumentException)
                { return; }

                this._unduel(pp.PraxisId);
            }

            this._voteDel.DeleteByPraxisParticipant(ppId);
            base.Delete(ppId);
        }

        public override void Delete(Id ppId)
        {
            void f(Id id0) => this._delete(ppId);
            this.Transaction<Id>(f, ppId, true);
        }

        public override void DeleteByLeft(Id praxisId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("praxisId");
            throw new NotSupportedException("You cannot delete all participants of a praxis, that would leave no one assigned to it, which is a no-no.");
        }

        /// <remarks>
        /// This will not be able to detect and delete PPs that have the same
        /// praxis and character. Luckily, people cannot have several
        /// characters on the same praxis, so this is not an issue.
        /// </remarks>
        public override void DeleteByRight(Id charId)
        {
            void f(Id id0) => this._deleteByChar(id0, null);
            this.Transaction<Id>(f, charId, true);
        }

        /// <remarks>
        /// Yes, this op takes two DB queries instead of one while a serialized
        /// transaction is active, but I seriously doubt this method is even
        /// going to get used, so I am okay with it being twice as costly.
        /// </remarks>
        public override void DeleteByDTO(NoIdRelationDTO<Id, int, Id, int> dto)
        {
            void f(NoIdRelationDTO<Id, int, Id, int> dto0)
            {
                this.Delete(this._ppRepo.GetByDTO(dto));
            }
            this.Transaction<NoIdRelationDTO<Id, int, Id, int>>(f, dto, true);
        }
    }
}