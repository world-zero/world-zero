using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

// TODO: Create the force methods. I will also need PraxisDel to safely perform
//      these. For the methods themselves, I may want to have a helper that is
//      shared by the virtual and Force versions that has a bool depending on
//      how to act.

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityRelationCntDel"/>
    /// <remarks>
    /// A Praxis should always have at least one participant. As a result,
    /// these methods will throw an exception if they are going to remove a
    /// praxis' final participant. To delete a praxis alongside the last
    /// participant (if the participant is the last to be removed), use the
    /// methods starting in `Force`.
    /// </remarks>
    public class PraxisParticipantDel : IEntityRelationCntDel
    <
        PraxisParticipant,
        Praxis,
        Id,
        int,
        Character,
        Id,
        int,
        CntRelationDTO<Id, int, Id, int>
    >
    {
        protected IPraxisParticipantRepo _ppRepo
        { get { return (IPraxisParticipantRepo) this._relRepo; } }

        public PraxisParticipantDel(IPraxisParticipantRepo repo)
            : base(repo)
        { }

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
        /// This will not be able to detect and delete PPs that have the same
        /// praxis and character. Luckily, people cannot have several
        /// characters on the same praxis, so this is not an issue.
        /// </remarks>
        public override void DeleteByPartialDTO(
            RelationDTO<Id, int, Id, int> dto
        )
        {
            void f(RelationDTO<Id, int, Id, int> dto0)
            {
                var dtos = this._ppRepo.GetParticipantCountsViaPartialDTO(dto);
                foreach (CountingDTO<Id> countDto in dtos)
                {
                    int endCount = countDto.Count - 1;
                    if (endCount < 1)
                        throw new ArgumentException($"Could not finish deletion, it would leave no participants on praxis {countDto.Countee.Get}.");
                }
                base.DeleteByPartialDTO(dto0);
            }
            this.Transaction<RelationDTO<Id, int, Id, int>>(f, dto, true);
        }

        /// <remarks>
        /// Yes, this op takes two DB queries instead of one while a serialized
        /// transaction is active, but I seriously doubt this method is even
        /// going to get used, so I am okay with it being twice as costly.
        /// </remarks>
        public override void DeleteByDTO(CntRelationDTO<Id, int, Id, int> dto)
        {
            void f(CntRelationDTO<Id, int, Id, int> dto0)
            {
                this.Delete(this._ppRepo.GetByDTO(dto));
            }
            this.Transaction<CntRelationDTO<Id, int, Id, int>>(f, dto, true);
        }
    }
}