using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityRelationDel"/>
    /// <remarks>
    /// A Praxis should always have at least one participant. As a result,
    /// these methods will throw an exception if they are going to remove a
    /// praxis' final participant.
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

        protected readonly VoteDel _voteDel;

        public PraxisParticipantDel(
            IPraxisParticipantRepo repo,
            VoteDel voteDel
        )
            : base(repo)
        {
            this.AssertNotNull(voteDel, "voteDel");
            this._voteDel = voteDel;
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

        public override void Delete(Id ppId)
        {
            void f(Id id0)
            {
                int endCount =
                    this._ppRepo.GetParticipantCountViaPPId(ppId) - 1;
                if (endCount < 1)
                    throw new ArgumentException($"Could not finish deletion, it would leave no participants on praxis {ppId.Get}.");
                this._voteDel.DeleteByPraxisParticipant(ppId);
                base.Delete(id0);
            }
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