using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Interface.Entity.Update.Primary
{
    /// <remarks>
    /// This intentionally does not have the ability to transfer a character
    /// between players.
    /// <br />
    /// This intentionally does not have the ability to update a character's
    /// Era or Total level since that is computed on-demand and not stored in
    /// the entity.
    /// </remarks>
    /// <inheritdoc cref="IIdNamedEntityUpdate{TEntity}"/>
    public interface ICharacterUpdate : IIdNamedEntityUpdate<ICharacter>
    {
        void AmendHasBio(ICharacter c, bool newHasBio);
        void AmendHasBio(Id charId, bool newHasBio);
        Task AmendHasBioAsync(ICharacter c, bool newHasBio);
        Task AmendHasBioAsync(Id charId, bool newHasBio);

        void AmendVotePointsLeft(ICharacter c, PointTotal newVotePointsLeft);
        void AmendVotePointsLeft(Id charId, PointTotal newVotePointsLeft);
        Task AmendVotePointsLeftAsync(ICharacter c, PointTotal newVotePointsLeft);
        Task AmendVotePointsLeftAsync(Id charId, PointTotal newVotePointsLeft);

        void AmendEraPointsLeft(ICharacter c, PointTotal newEraPointsLeft);
        void AmendEraPointsLeft(Id charId, PointTotal newEraPointsLeft);
        Task AmendEraPointsLeftAsync(ICharacter c, PointTotal newEraPointsLeft);
        Task AmendEraPointsLeftAsync(Id charId, PointTotal newEraPointsLeft);

        void AmendTotalPointsLeft(ICharacter c, PointTotal newTotalPointsLeft);
        void AmendTotalPointsLeft(Id charId, PointTotal newTotalPointsLeft);
        Task AmendTotalPointsLeftAsync(ICharacter c, PointTotal newTotalPointsLeft);
        Task AmendTotalPointsLeftAsync(Id charId, PointTotal newTotalPointsLeft);

        void AmendHasProfilePic(ICharacter c, bool newHasProfilePic);
        void AmendHasProfilePic(Id charId, bool newHasProfilePic);
        Task AmendHasProfilePicAsync(ICharacter c, bool newHasProfilePic);
        Task AmendHasProfilePicAsync(Id charId, bool newHasProfilePic);

        void AmendFaction(ICharacter c, IFaction f);
        void AmendFaction(Id charId, IFaction f);
        void AmendFaction(ICharacter c, Name factionId);
        void AmendFaction(Id charId, Name factionId);
        Task AmendFactionAsync(ICharacter c, IFaction f);
        Task AmendFactionAsync(Id charId, IFaction f);
        Task AmendFactionAsync(ICharacter c, Name factionId);
        Task AmendFactionAsync(Id charId, Name factionId);

        void AmendLocation(ICharacter c, ILocation l);
        void AmendLocation(Id charId, ILocation l);
        void AmendLocation(ICharacter c, Id locationId);
        void AmendLocation(Id charId, Id locationId);
        Task AmendLocationAsync(ICharacter c, ILocation l);
        Task AmendLocationAsync(Id charId, ILocation l);
        Task AmendLocationAsync(ICharacter c, Id locationId);
        Task AmendLocationAsync(Id charId, Id locationId);
    }
}