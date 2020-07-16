using WorldZero.Data;
using WorldZero.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using NUnit.Framework;

// I wouldn't call this a bulletproof test by any means, but it will do a
// pretty decent job of making sure the DbContext doesn't have any runtime
// exceptions.

namespace WorldZero.Test.Integration
{
    [TestFixture]
    class TestW0Context
    {
        private W0Context _context;
        private string _contextString;

        [OneTimeSetUp]
        public void ResetDb()
        {
            // TODO: I am aware that this is poor and I'm fixing it later.
            this._contextString = "Data Source=.;Server=localhost;Database=W0Test;User Id=fake;Password=extrafake;";
            this._context = new W0Context(this._contextString);
        }

        [Test]
        public void RuntimeChecker()
        {
            var newLocal = new Location
            {
                City = "oregon city",
                State = "oregon",
                Country = "USA",
                Zip = "97045"
            };
            var otherLocal = new Location
            {
                City = "west linn",
                State = "oregon",
                Country = "USA",
                Zip = "69420"
            };
            var newPlayer = new Player() { Username="Sawyer" };
            // The PlayerId is set to 0 by default by compiler, so EFC
            // starts counting IDs at 1.
            var otherPlayer = new Player() { Username="Zel" };
            var newFaction = new Faction()
            {
                FactionName = "DIO",
                Description = "The vampire from Jojo's bizarre adventure",
                AbilityName = "The World",
                AbilityDesc = "Stop time"
            };

            var newEra = new Era() { EraName = "The Beginning" };
            var newStatus = new Status() { StatusName = "Incomplete" };
            var newFlag = new Flag() { FlagName = "Gross" };
            var newTag = new Tag() { TagName = "#pizza" };
            var newTask = new Task()
            {
                Summary = "test task",
                Points = 3,
                Level = 1,
                FactionName = newFaction.FactionName,
                StatusName = newStatus.StatusName
            };

            this._context.Locations.Add(newLocal);
            this._context.Locations.Add(otherLocal);
            this._context.Players.Add(newPlayer);
            this._context.Players.Add(otherPlayer);
            this._context.Factions.Add(newFaction);
            this._context.Eras.Add(newEra);
            this._context.Statuses.Add(newStatus);
            this._context.Flags.Add(newFlag);
            this._context.Tags.Add(newTag);
            this._context.Tasks.Add(newTask);
            this._context.SaveChanges();

            // Automatic IDs are set on store, just like w/ Dapper.Contrib
            //Console.WriteLine($"{newPlayer.PlayerId}"); // 1
            //Console.WriteLine($"{newLocal.LocationId}"); // 1
            //Console.WriteLine($"{otherLocal.LocationId}"); // 2

            var newChar = new Character()
            {
                Displayname = "Sawyer's Character",
                PlayerId = newPlayer.PlayerId,
                Location = newLocal,
                FactionName = newFaction.FactionName
            };
            this._context.Characters.Add(newChar);
            this._context.SaveChanges();
            //Console.WriteLine(newChar.FactionName);
            // We can just grab the first since that's the key.
            foreach (Character c in this._context.Factions.Where(f => f.FactionName == "DIO").First().Members)
            {
                // These both work, but do slightly different things.
                //Console.WriteLine($"DIO minion: {c.PlayerId}@{c.Displayname}");
                //Console.WriteLine($"DIO minion: {c.Player.Username}@{c.Displayname}");


                //Console.WriteLine(c.Player); // Player
            }

            // Get a Location on some criteria.
            var location = this._context.Locations.Where(l => l.LocationId == 1).Single();
            //Console.WriteLine($"{location.LocationId} {location.City} {location.State}");

            // How do I access/update the manys?
            // EFC will convert and populate this into a HashSet for me.
            // Since these are all shallow copies to the things in the this._context, changes are recorded.
            // NOTE: classes that load these mementos should be sure to not break this
            Player player = this._context.Players.First();
            foreach (Character c in player.Characters)
                c.EraPoints = 10;
            //Console.WriteLine(player.Characters);

            // since it auto-tracks changes by default, it will store this change.
            player.IsBlocked = true;
            this._context.SaveChanges();

            // You cannot change a part of something's key, as you would imagine.

            // In the DB, Faction is stored on the character, but you can still edit Faction.Members
            // and have the changes be recorded..
            newFaction.Members.Remove(newChar);
            this._context.SaveChanges();

            // To load a collection that is a many-to-many (w/o a m-t-m model) you need to do some shit.
            // You need to eagerly load the collection - this is really janky.
            var xTask = this._context.Tasks
                                    .Include(t => t.Flags) // or:  => t.Flags == somethi
                                    .FirstOrDefault();
            xTask.Flags.Add(newFlag);
            this._context.SaveChanges();


            // You can set a releation w/ either the foriegn key or the object.
            var newMetaTask = new MetaTask()
            {
                FactionName = newFaction.FactionName,
                MetaTaskName = "Pizza",
                Description = "Eat a pizza too.",
                Bonus = 100000,
                StatusName = newStatus.StatusName
            };
            this._context.MetaTasks.Add(newMetaTask);
            this._context.SaveChanges();
            var newPraxis = new Praxis()
            {
                Task = newTask,
                AreDueling = false,
                Status = newStatus,
                Collaborators = new HashSet<Character>(),
                MetaTasks = new HashSet<MetaTask>()
            };
            newPraxis.Collaborators.Add(newChar);
            newPraxis.MetaTasks.Add(newMetaTask);
            this._context.Praxises.Add(newPraxis);
            this._context.SaveChanges();

            var newVote = new Vote()
            {
                PraxisId = newPraxis.PraxisId,
                CharacterId = newChar.CharacterId,
                Points = 3
            };
            this._context.Votes.Add(newVote);
            this._context.SaveChanges();

            var newComment = new Comment()
            {
                PraxisId = newPraxis.PraxisId,
                CharacterId = newChar.CharacterId,
                Value = "This looks great!"
            };
            this._context.Comments.Add(newComment);
            this._context.SaveChanges();
        }
    }
}
