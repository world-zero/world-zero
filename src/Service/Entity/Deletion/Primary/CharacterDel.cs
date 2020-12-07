using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel"/>
    public class CharacterDel : IEntityDel<UnsafeCharacter, Id, int>
    {
        protected IUnsafeCharacterRepo _charRepo
        { get { return (IUnsafeCharacterRepo) this._repo; } }

        protected readonly FriendDel _friendDel;
        protected readonly FoeDel _foeDel;
        protected readonly CommentDel _commentDel;
        protected readonly VoteDel _voteDel;
        protected readonly PraxisDel _praxisDel;
        protected readonly PraxisParticipantDel _ppDel;

        public CharacterDel(
            IUnsafeCharacterRepo charRepo,
            FriendDel friendDel,
            FoeDel foeDel,
            CommentDel commentDel,
            VoteDel voteDel,
            PraxisDel praxisDel,
            PraxisParticipantDel ppDel
        )
            : base(charRepo)
        {
            this.AssertNotNull(commentDel, "commentDel");
            this.AssertNotNull(friendDel, "friendDel");
            this.AssertNotNull(foeDel, "foeDel");
            this.AssertNotNull(voteDel, "voteDel");
            this.AssertNotNull(praxisDel, "praxisDel");
            this.AssertNotNull(ppDel, "ppDel");

            this._friendDel = friendDel;
            this._foeDel = foeDel;
            this._commentDel = commentDel;
            this._voteDel = voteDel;
            this._praxisDel = praxisDel;
            this._ppDel = ppDel;
        }

        public override void Delete(Id charId)
        {
            void op(Id id)
            {
                this._friendDel .DeleteByCharacter(id);
                this._foeDel    .DeleteByCharacter(id);
                this._commentDel.DeleteByCharacter(id);
                this._voteDel   .DeleteByCharacter(id);
                this._ppDel .SudoDeleteByCharacter(id, this._praxisDel);
                base.Delete(id);
            }

            this.Transaction<Id>(op, charId, true);
        }

        public void DeleteByPlayer(Player p)
        {
            this.AssertNotNull(p, "p");
            this.DeleteByPlayer(p.Id);
        }

        public void DeleteByPlayer(Id playerId)
        {
            this.AssertNotNull(playerId, "playerId");
            void f(Id id)
            {
                IEnumerable<UnsafeCharacter> chars;
                try
                {
                    chars = this._charRepo.GetByPlayerId(playerId);
                    chars.Count();
                }
                catch (ArgumentException)
                { return; }
                try
                {
                    foreach (UnsafeCharacter c in chars)
                        this.Delete(c.Id);
                }
                catch (ArgumentException e)
                { throw new ArgumentException("Could not complete the deletion.", e); }
            }
            this.Transaction<Id>(f, playerId, true);
        }
 
        public async
        System.Threading.Tasks.Task DeleteByPlayerAsync(Player p)
        {
            this.AssertNotNull(p, "p");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPlayer(p));
        }

        public async
        System.Threading.Tasks.Task DeleteByPlayerAsync(Id playerId)
        {
            this.AssertNotNull(playerId, "playerId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByPlayer(playerId));
        }
   }
}
