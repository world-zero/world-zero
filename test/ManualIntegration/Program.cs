using WorldZero.Data;
using WorldZero.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace WorldZero.Test.ManualIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the manual integration tests for World Zero!");
            Console.WriteLine();
            using (var context = new W0Context("name=TestConnection"))
            {
                Console.WriteLine("Currently, this is only an EF Core build and CRUD test.");
                Console.WriteLine("\tI wouldn't call this a bulletproof test, but if it runs without EF Core throwing a thousand exceptions, your DbContext should be pretty free of runtime exceptions.");
                Console.WriteLine();
                context.Database.Delete();

                Console.WriteLine("Starting EF Core code-first database creation.");
                Console.WriteLine("This test will take a bit.");
                RuntimeChecker(context);
                Console.WriteLine("Done.");
            }
        }

        static void RuntimeChecker(W0Context context)
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

            var newEra = new EraModel() { EraName = "The Beginning" };
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

            context.Locations.Add(newLocal);
            context.Locations.Add(otherLocal);
            context.Players.Add(newPlayer);
            context.Players.Add(otherPlayer);
            context.Factions.Add(newFaction);
            context.Eras.Add(newEra);
            context.Statuses.Add(newStatus);
            context.Flags.Add(newFlag);
            context.Tags.Add(newTag);
            context.Tasks.Add(newTask);
            context.SaveChanges();

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
            context.Characters.Add(newChar);
            context.SaveChanges();
            //Console.WriteLine(newChar.FactionName);
            // We can just grab the first since that's the key.
            foreach (Character c in context.Factions.Where(f => f.FactionName == "DIO").First().Members)
            {
                // These both work, but do slightly different things.
                //Console.WriteLine($"DIO minion: {c.PlayerId}@{c.Displayname}");
                //Console.WriteLine($"DIO minion: {c.Player.Username}@{c.Displayname}");


                //Console.WriteLine(c.Player); // Player
            }

            // Get a Location on some criteria.
            var location = context.Locations.Where(l => l.LocationId == 1).Single();
            //Console.WriteLine($"{location.LocationId} {location.City} {location.State}");

            // How do I access/update the manys?
            // EFC will convert and populate this into a HashSet for me.
            // Since these are all shallow copies to the things in the context, changes are recorded.
            // NOTE: classes that load these mementos should be sure to not break this
            Player player = context.Players.First();
            foreach (Character c in player.Characters)
                c.EraPoints = 10;
            //Console.WriteLine(player.Characters);

            // since it auto-tracks changes by default, it will store this change.
            player.IsBlocked = true;
            context.SaveChanges();

            // You cannot change a part of something's key, as you would imagine.

            // In the DB, Faction is stored on the character, but you can still edit Faction.Members
            // and have the changes be recorded..
            newFaction.Members.Remove(newChar);
            context.SaveChanges();

            // To load a collection that is a many-to-many (w/o a m-t-m model) you need to do some shit.
            // You need to eagerly load the collection - this is really janky.
            var xTask = context.Tasks
                                    .Include(t => t.Flags) // or:  => t.Flags == somethi
                                    .FirstOrDefault();
            xTask.Flags.Add(newFlag);
            context.SaveChanges();


            // You can set a releation w/ either the foriegn key or the object.
            var newMetaTask = new MetaTask()
            {
                FactionName = newFaction.FactionName,
                MetaTaskName = "Pizza",
                Description = "Eat a pizza too.",
                Bonus = 100000,
                StatusName = newStatus.StatusName
            };
            context.MetaTasks.Add(newMetaTask);
            context.SaveChanges();
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
            context.Praxises.Add(newPraxis);
            context.SaveChanges();

            var newVote = new Vote()
            {
                PraxisId = newPraxis.PraxisId,
                CharacterId = newChar.CharacterId,
                Points = 3
            };
            context.Votes.Add(newVote);
            context.SaveChanges();

            var newComment = new Comment()
            {
                PraxisId = newPraxis.PraxisId,
                CharacterId = newChar.CharacterId,
                Value = "This looks great!"
            };
            context.Comments.Add(newComment);
            context.SaveChanges();
        }
    }
}
