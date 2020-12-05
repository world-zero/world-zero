using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("WorldZero.Test.Integration")]

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityRelationDel"/>
    /// <remarks>
    /// A Praxis should always have at least one participant. As a result,
    /// these methods will throw an exception if they are going to remove a
    /// praxis' final participant.
    /// <br />
    /// If a participant of a duel is deleted, then the praxis will be updated
    /// to no longer be a duel. This does not use the praxis updating service
    /// class.
    /// <br />
    /// This will also delete the participant's received votes. Whether or not
    /// the voting character will receive a refund is up to <see
    /// cref="VoteDel"/>.
    /// </remarks>
    public class PraxisParticipantDel : IEntityRelationDel
    <
        PraxisParticipant,
        Praxis,
        Id,
        int,
        Character,
        Id,
        int,
        RelationDTO<Id, int, Id, int>
    >
    {
        protected IPraxisParticipantRepo _ppRepo
        { get { return (IPraxisParticipantRepo) this._relRepo; } }

        protected readonly IPraxisRepo _praxisRepo;
        protected readonly VoteDel _voteDel;

        public PraxisParticipantDel(
            IPraxisParticipantRepo repo,
            IPraxisRepo praxisRepo,
            VoteDel voteDel
        )
            : base(repo)
        {
            this.AssertNotNull(praxisRepo, "praxisRepo");
            this.AssertNotNull(voteDel, "voteDel");

            this._praxisRepo = praxisRepo;
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
        internal void UNSAFE_DeleteByPraxis(Praxis p)
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
                IEnumerable<PraxisParticipant> pps;
                try
                {
                    pps = this._ppRepo.GetByPraxisId(id);
                    foreach (PraxisParticipant pp in pps)
                        this._delete(pp.Id, false);
                }
                catch (ArgumentException)
                { return; }
            }
            this.Transaction<Id>(f, praxisId, true);
        }

        public void DeleteByPraxis(Praxis p)
        {
            this.DeleteByLeft(p);
        }

        public void DeleteByPraxis(Id id)
        {
            this.DeleteByLeft(id);
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisAsync(Praxis p)
        {
            this.AssertNotNull(p, "p");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxis(p));
        }

        public async
        System.Threading.Tasks.Task DeleteByPraxisAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPraxis(id));
        }

        public void DeleteByCharacter(Character c)
        {
            this.DeleteByRight(c);
        }

        public void DeleteByCharacter(Id id)
        {
            this.DeleteByRight(id);
        }

        public async
        System.Threading.Tasks.Task DeleteByCharacterAsync(Character c)
        {
            this.AssertNotNull(c, "c");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByCharacter(c));
        }

        public async
        System.Threading.Tasks.Task DeleteByCharacterAsync(Id id)
        {
            this.AssertNotNull(id, "id");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByCharacter(id));
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
                PraxisParticipant pp;
                try
                { pp = this._ppRepo.GetById(ppId); }
                catch (ArgumentException)
                { return; }

                Praxis p;
                try
                {
                    p = this._praxisRepo.GetById(pp.PraxisId);
                }
                catch (ArgumentException)
                { throw new InvalidOperationException("There exists PraxisParticipant(s) for a Praxis that does not exist."); }

                if (p.AreDueling)
                {
                    p.AreDueling = false;
                    this._praxisRepo.Update(p);
                }
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
            void f(Id Id0)
            {
                var dtos = this._ppRepo.GetParticipantCountsViaCharId(charId);
                foreach (CountingDTO<Id> dto in dtos)
                {
                    int endCount = dto.Count - 1;
                    if (endCount < 1)
                        throw new ArgumentException($"Could not finish deletion, it would leave no participants on praxis {dto.Countee.Get}.");
                }
                base.DeleteByRight(charId);
            }
            this.Transaction<Id>(f, charId, true);
        }

        /// <remarks>
        /// Yes, this op takes two DB queries instead of one while a serialized
        /// transaction is active, but I seriously doubt this method is even
        /// going to get used, so I am okay with it being twice as costly.
        /// </remarks>
        public override void DeleteByDTO(RelationDTO<Id, int, Id, int> dto)
        {
            void f(RelationDTO<Id, int, Id, int> dto0)
            {
                this.Delete(this._ppRepo.GetByDTO(dto));
            }
            this.Transaction<RelationDTO<Id, int, Id, int>>(f, dto, true);
        }
    }
}