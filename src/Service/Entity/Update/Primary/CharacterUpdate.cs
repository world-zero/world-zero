using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="ICharacterUpdate"/>
    public class CharacterUpdate
        : ABCIdNamedEntityUpdate<ICharacter>,
        ICharacterUpdate
    {
        protected readonly IFactionRepo _factionRepo;
        protected readonly ILocationRepo _locationRepo;

        public CharacterUpdate(
            ICharacterRepo repo,
            IFactionRepo factionRepo,
            ILocationRepo locationRepo
        )
            : base(repo)
        {
            this.AssertNotNull(factionRepo, "factionRepo");
            this.AssertNotNull(locationRepo, "locationRepo");
            this._factionRepo = factionRepo;
            this._locationRepo = locationRepo;
        }

        // --------------------------------------------------------------------

        public void AmendHasBio(ICharacter c, bool newHasBio)
        {
            this.AssertNotNull(c, "c");
            void f()
            {
                ((UnsafeCharacter) c).HasBio = newHasBio;
            }
            this.AmendHelper<ICharacter>(f, c);
        }

        public void AmendHasBio(Id charId, bool newHasBio)
        {
            this.AssertNotNull(charId, "charId");
            void f<Id>(Id _)
            {
                this.AmendHasBio(this._repo.GetById(charId), newHasBio);
            }
            this.Transaction(f, true);
        }

        public async Task AmendHasBioAsync(ICharacter c, bool newHasBio)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.AmendHasBio(c, newHasBio));
        }

        public async Task AmendHasBioAsync(Id charId, bool newHasBio)
        {
            this.AssertNotNull(charId, "charId");
            await Task.Run(() => this.AmendHasBio(charId, newHasBio));
        }

        // --------------------------------------------------------------------

        public void AmendHasProfilePic(ICharacter c, bool newHasProfilePic)
        {
            this.AssertNotNull(c, "c");
            void f()
            {
                ((UnsafeCharacter) c).HasProfilePic = newHasProfilePic;
            }
            this.AmendHelper<ICharacter>(f, c);
        }

        public void AmendHasProfilePic(Id charId, bool newHasProfilePic)
        {
            this.AssertNotNull(charId, "charId");
            void f<Id>(Id _)
            {
                this.AmendHasProfilePic(
                    this._repo.GetById(charId),
                    newHasProfilePic
                );
            }
            this.Transaction(f, true);
        }

        public async
        Task AmendHasProfilePicAsync(ICharacter c, bool newHasProfilePic)
        {
            this.AssertNotNull(c, "c");
            await Task.Run(() => this.AmendHasProfilePic(c, newHasProfilePic));
        }

        public async
        Task AmendHasProfilePicAsync(Id charId, bool newHasProfilePic)
        {
            this.AssertNotNull(charId, "charId");
            await Task.Run(() =>
                this.AmendHasProfilePic(charId, newHasProfilePic));
        }

        // --------------------------------------------------------------------

        public void AmendVotePointsLeft(
            ICharacter c,
            PointTotal newVotePointsLeft
        )
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newVotePointsLeft, "newVotePointsLeft");
            void f()
            {
                ((UnsafeCharacter) c).VotePointsLeft = newVotePointsLeft;
            }
            this.AmendHelper<ICharacter>(f, c);
        }

        public
        void AmendVotePointsLeft(Id charId, PointTotal newVotePointsLeft)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newVotePointsLeft, "newVotePointsLeft");
            void f<Id>(Id _)
            {
                this.AmendVotePointsLeft(this._repo.GetById(charId), newVotePointsLeft);
            }
            this.Transaction(f, true);
        }

        public async Task AmendVotePointsLeftAsync(
            ICharacter c,
            PointTotal newVotePointsLeft
        )
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newVotePointsLeft, "newVotePointsLeft");
            await Task.Run(() => this.AmendVotePointsLeft(c, newVotePointsLeft));
        }

        public async Task AmendVotePointsLeftAsync(
            Id charId,
            PointTotal newVotePointsLeft
        )
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newVotePointsLeft, "newVotePointsLeft");
            await Task.Run(() => this.AmendVotePointsLeft(charId, newVotePointsLeft));
        }

        // --------------------------------------------------------------------

        public void AmendEraPoints(ICharacter c, PointTotal newEraPoints)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newEraPoints, "newEraPoints");
            void f()
            {
                ((UnsafeCharacter) c).EraPoints = newEraPoints;
            }
            this.AmendHelper<ICharacter>(f, c);
        }

        public void AmendEraPoints(Id charId, PointTotal newEraPoints)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newEraPoints, "newEraPoints");
            void f<Id>(Id _)
            {
                this.AmendEraPoints(this._repo.GetById(charId), newEraPoints);
            }
            this.Transaction(f, true);
        }

        public async Task AmendEraPointsAsync(
            ICharacter c,
            PointTotal newEraPoints
        )
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newEraPoints, "newEraPoints");
            await Task.Run(() => this.AmendEraPoints(c, newEraPoints));
        }

        public async Task AmendEraPointsAsync(
            Id charId,
            PointTotal newEraPoints
        )
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newEraPoints, "newEraPoints");
            await Task.Run(() => this.AmendEraPoints(charId, newEraPoints));
        }

        // --------------------------------------------------------------------

        public void AmendTotalPoints(ICharacter c, PointTotal newTotalPoints)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newTotalPoints, "newTotalPoints");
            void f()
            {
                ((UnsafeCharacter) c).TotalPoints = newTotalPoints;
            }
            this.AmendHelper<ICharacter>(f, c);
        }

        public void AmendTotalPoints(Id charId, PointTotal newTotalPoints)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newTotalPoints, "newTotalPoints");
            void f<Id>(Id _)
            {
                this.AmendTotalPoints(this._repo.GetById(charId), newTotalPoints);
            }
            this.Transaction(f, true);
        }

        public async Task AmendTotalPointsAsync(
            ICharacter c,
            PointTotal newTotalPoints
        )
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newTotalPoints, "newTotalPoints");
            await Task.Run(() => this.AmendTotalPoints(c, newTotalPoints));
        }

        public async Task AmendTotalPointsAsync(
            Id charId,
            PointTotal newTotalPoints
        )
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newTotalPoints, "newTotalPoints");
            await Task.Run(() => this.AmendTotalPoints(charId, newTotalPoints));
        }

        // --------------------------------------------------------------------

        public void AmendFaction(ICharacter c, IFaction newFaction)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newFaction, "newFaction");
            void f()
            {
                Name newFactionId = newFaction.Id;
                this._factionRepo.GetById(newFactionId);
                ((UnsafeCharacter) c).FactionId = newFactionId;
            }
            this.AmendHelper<ICharacter>(f, c, true);
        }

        public void AmendFaction(ICharacter c, Name newFactionId)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newFactionId, "newFactionId");
            void f()
            {
                this._factionRepo.GetById(newFactionId);
                ((UnsafeCharacter) c).FactionId = newFactionId;
            }
            this.AmendHelper<ICharacter>(f, c, true);
        }

        public void AmendFaction(Id charId, IFaction newFaction)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newFaction, "newFaction");
            void f<Id>(Id _)
            {
                this.AmendFaction(this._repo.GetById(charId), newFaction);
            }
            this.Transaction(f, true);
        }

        public void AmendFaction(Id charId, Name newFactionId)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newFactionId, "newFactionId");
            void f<Id>(Id _)
            {
                this.AmendFaction(this._repo.GetById(charId), newFactionId);
            }
            this.Transaction(f, true);
        }

        public async Task AmendFactionAsync(ICharacter c, IFaction newFaction)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newFaction, "newFaction");
            await Task.Run(() => this.AmendFaction(c, newFaction));
        }

        public async Task AmendFactionAsync(ICharacter c, Name newFactionId)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newFactionId, "newFactionId");
            await Task.Run(() => this.AmendFaction(c, newFactionId));
        }

        public async Task AmendFactionAsync(Id charId, IFaction newFaction)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newFaction, "newFaction");
            await Task.Run(() => this.AmendFaction(charId, newFaction));
        }

        public async Task AmendFactionAsync(Id charId, Name newFactionId)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newFactionId, "newFactionId");
            await Task.Run(() => this.AmendFaction(charId, newFactionId));
        }

        // --------------------------------------------------------------------

        public void AmendLocation(ICharacter c, ILocation newLocation)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newLocation, "newLocation");
            void f()
            {
                Id newLocationId = newLocation.Id;
                this._locationRepo.GetById(newLocationId);
                ((UnsafeCharacter) c).LocationId = newLocationId;
            }
            this.AmendHelper<ICharacter>(f, c, true);
        }

        public void AmendLocation(ICharacter c, Id newLocationId)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newLocationId, "newLocationId");
            void f()
            {
                this._locationRepo.GetById(newLocationId);
                ((UnsafeCharacter) c).LocationId = newLocationId;
            }
            this.AmendHelper<ICharacter>(f, c, true);
        }

        public void AmendLocation(Id charId, ILocation newLocation)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newLocation, "newLocation");
            void f<Id>(Id _)
            {
                this.AmendLocation(this._repo.GetById(charId), newLocation);
            }
            this.Transaction(f, true);
        }

        public void AmendLocation(Id charId, Id newLocationId)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newLocationId, "newLocationId");
            void f<Id>(Id _)
            {
                this.AmendLocation(this._repo.GetById(charId), newLocationId);
            }
            this.Transaction(f, true);
        }

        public async Task AmendLocationAsync(ICharacter c, ILocation newLocation)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newLocation, "newLocation");
            await Task.Run(() => this.AmendLocation(c, newLocation));
        }

        public async Task AmendLocationAsync(ICharacter c, Id newLocationId)
        {
            this.AssertNotNull(c, "c");
            this.AssertNotNull(newLocationId, "newLocationId");
            await Task.Run(() => this.AmendLocation(c, newLocationId));
        }

        public async Task AmendLocationAsync(Id charId, ILocation newLocation)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newLocation, "newLocation");
            await Task.Run(() => this.AmendLocation(charId, newLocation));
        }

        public async Task AmendLocationAsync(Id charId, Id newLocationId)
        {
            this.AssertNotNull(charId, "charId");
            this.AssertNotNull(newLocationId, "newLocationId");
            await Task.Run(() => this.AmendLocation(charId, newLocationId));
        }
    }
}