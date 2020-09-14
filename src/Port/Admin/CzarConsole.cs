using System;
using Microsoft.Extensions.DependencyInjection;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Service.Entity.Registration;
using WorldZero.Port.Interface;

// NOTE: Most of these are not done (at the time of writing) due to lack of
// sufficient service classes. Before beginning a method's implementation, make
// sure the needed service classes exist.

namespace WorldZero.Port.Admin
{
    /// <summary>
    /// This is the admin's all-powerful controller.
    /// </summary>
    public class CzarConsole
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ITerminal _terminal;
        protected readonly AbilityRegistration _abilityRegistration;
        protected readonly CharacterRegistration _characterRegistration;
        protected readonly EraRegistration _eraRegistration;
        protected readonly FactionRegistration _factionRegistration;
        protected readonly FlagRegistration _FlagRegistration;
        protected readonly LocationRegistration _LocationRegistration;
        protected readonly MetaTaskRegistration _metaTaskRegistration;
        protected readonly PlayerRegistration _playerRegistration;
        protected readonly PraxisRegistration _praxisRegistration;
        protected readonly StatusRegistration _statusRegistration;
        protected readonly TagRegistration _tagRegistration;
        protected readonly TaskRegistration _taskRegistration;

        /// <summary>
        /// Provide administrators with a means of controlling the system.
        /// </summary>
        /// <param name="serviceProvider">
        /// An IServiceProvider that contains a multitude of needed services.
        /// If any are not configured, then an exception will be thrown. For a
        /// full listing, read the constructor's code.
        /// </param>
        public CzarConsole(
            IServiceProvider serviceProvider
        )
        {
            if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");

            this._serviceProvider = serviceProvider;
            try
            {
                this._terminal = this._serviceProvider
                    .GetRequiredService<ITerminal>();
                this._characterRegistration = this._serviceProvider
                    .GetRequiredService<CharacterRegistration>();
                this._eraRegistration = this._serviceProvider
                    .GetRequiredService<EraRegistration>();
                this._factionRegistration = this._serviceProvider
                    .GetRequiredService<FactionRegistration>();
                this._FlagRegistration = this._serviceProvider
                    .GetRequiredService<FlagRegistration>();
                this._LocationRegistration = this._serviceProvider
                    .GetRequiredService<LocationRegistration>();
                this._metaTaskRegistration = this._serviceProvider
                    .GetRequiredService<MetaTaskRegistration>();
                this._playerRegistration = this._serviceProvider
                    .GetRequiredService<PlayerRegistration>();
                this._praxisRegistration = this._serviceProvider
                    .GetRequiredService<PraxisRegistration>();
                this._statusRegistration = this._serviceProvider
                    .GetRequiredService<StatusRegistration>();
                this._tagRegistration = this._serviceProvider
                    .GetRequiredService<TagRegistration>();
                this._taskRegistration = this._serviceProvider
                    .GetRequiredService<TaskRegistration>();
            }
            catch (InvalidOperationException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        /// <summary>
        /// This method will populate and save the repositories with the
        /// pre-defined defaults. The era is defined as "The Beginning". This
        /// method assumes that there will be no conflicts with existing repo
        /// data, so it is highly recommended to only run this method with
        /// empty repositories.
        ///
        /// WARNING; Due to this being a setup tool, it is not multi-threaded
        /// safe.
        /// </summary>
        public void InitRepos()
        {
            this._eraRegistration.Register(new Name("The Beginning"));

            // TODO: The plan is to move these into config files, but this
            // works for now.
            this._setupBaseStatuses();
            this._setupFlags();
            this._setupAbilitiesFactions();
        }

        /// <remarks>
        /// Do NOT change these, other things rely on these names to be as
        /// they are.
        /// </remarks>
        private void _setupBaseStatuses()
        {
            this._statusRegistration.Register(
                new Status(
                    new Name("Pretired"),
                    "This is generally only to be used by proposed tasks."));
            this._statusRegistration.Register(new Status(new Name("Active")));
            this._statusRegistration.Register(new Status(new Name("Retired")));
            this._statusRegistration.Register(new Status(new Name("In Progress")));
        }

        private void _setupFlags()
        {
            this._FlagRegistration.Register(new Flag(new Name("Dangerous")));
            this._FlagRegistration.Register(new Flag(
                new Name("Inappropriate")));
            this._FlagRegistration.Register(new Flag(new Name("Misogynistic")));
            this._FlagRegistration.Register(new Flag(new Name("Racist")));
            this._FlagRegistration.Register(new Flag(new Name("Homophobic")));
            this._FlagRegistration.Register(new Flag(new Name("Transphobic")));
            this._FlagRegistration.Register(new Flag(new Name("LGBT+ -phobic")));
            this._FlagRegistration.Register(new Flag(new Name("Classist")));
            this._FlagRegistration.Register(new Flag(new Name("Fatphobic")));
        }

        private void _setupAbilitiesFactions()
        {
            // These are not hardcoded or in classes to allow for
            // database-driven correlations. This is also why this method isn't
            // broken into helpers.
            PastDate now = new PastDate(DateTime.UtcNow);
            Ability ability;

            ability = new Ability(
                new Name("Beginner's Bonus"), 
                String.Join(" ",
                    "You get 100% points on all tasks you do. You can sign up",
                    "for a task from any group. When you do, if you are level",
                    "2, you will get a “welcome letter” from that group upon",
                    "completion of a task from that group. At 3, you have to",
                    "choose one of the other available groups."
                )
            );
            this._abilityRegistration.Register(ability);
            this._factionRegistration.Register(new Faction(
                new Name("UA"), now, null, ability.Id
            ));

            ability = new Ability(
                new Name("Jack of all Trades"),
                "You complete tasks for all factions at 80% of the points."
            );
            this._abilityRegistration.Register(ability);
            this._factionRegistration.Register(new Faction(
                new Name("UA Masters"), now, null, ability.Id
            ));

            ability = new Ability(
                new Name("Competitor"), 
                "You will receive an extra 10% of points from duels."
            );
            this._abilityRegistration.Register(ability);
            this._factionRegistration.Register(new Faction(
                new Name("Snide"), now, null, ability.Id
            ));

            ability = new Ability(
                new Name("Collaborator"), 
                String.Join(" ",
                    "If you complete a task for your group, you receive 110%",
                    "of the points. If you do a task of another group, then",
                    "you receive 70% of the points." 
                )
            );
            this._abilityRegistration.Register(ability);
            this._factionRegistration.Register(new Faction(
                new Name("Gestalt"), now, null, ability.Id
            ));

            ability = new Ability(
                // NOTE: this name is used in other documentation annd in class
                // members, it is not recommended to change it.
                new Name("Historian"),
                "You may sign up for approved pretired / retired tasks."
            );
            this._abilityRegistration.Register(ability);
            this._factionRegistration.Register(new Faction(
                new Name("Journeymen"), now, null, ability.Id
            ));

            ability = new Ability(
                new Name("Reiterate"), 
                String.Join(" ",
                    "You may complete one task of each level a certain",
                    "number of extra times."
                )
            );
            this._abilityRegistration.Register(ability);
            this._factionRegistration.Register(new Faction(
                new Name("Analog"), now, null, ability.Id
            ));

            this._factionRegistration.Register(new Faction(
                new Name("Singularity"), now
            ));

            ability = new Ability(
                new Name("Open Season"), 
                String.Join(" ",
                    "You may complete any task with any meta task(s) from any",
                    "group for full points."
                )
            );
            this._abilityRegistration.Register(ability);
            this._factionRegistration.Register(new Faction(
                new Name("Albescent"), now, null, ability.Id
            ));
        }

        /// <summary>
        /// This method will purge and then populate and save the repositories
        /// with the pre-defined defaults. The era is defined as "The
        /// Beginning". To reiterate, this method will destroy the repositories
        /// as they are, guaranteeing they are in the default state. As you
        /// would hope, this will require input from the user to verify that
        /// they truly want to do this.
        /// </summary>
        public void ResetRepos()
        {
            throw new NotImplementedException();
            // TODO: ask them if they really want to reset, if repos are not empty
            //      this would also make sense as a method on ientityrepo
            //      see if there's a way to have a generic method that takes a method pointer
            //          or just have an iterable property to different repos

            // TODO: clean out repos
            //      this should be a method on IEntityRepo, if not IGenericRepo

            this.InitRepos();
        }

        /// <summary>
        /// This method will end the previous era and start a new era. For the
        /// finer details about the era start and end dates, see
        /// `EraRegistration.Register`.
        /// </summary>
        public void EraMigration()
        {
            // Because needed helper is not built.
            throw new NotImplementedException();

            string warning = String.Join(" ",
                "WARNING: You are about to migrate an era. This will move all",
                "players' era points and level into their total points and",
                "level, as well as retiring all active tasks.",
                "\n",
                "This action is IRREVERSIBLE.",
                "\n",
                "To continue this process, input the displayed number."
            );
            if (!this.GetConfirmation(warning))
            {
                this._terminal.Print("This function is cancelling.");
                return;
            }

            Name newName;
            try
            {
                newName = new Name(this._terminal
                    .PromptNotNull<string>("Enter the new era's name."));
            }
            catch (ArgumentNullException)
            {
                throw new InvalidOperationException("An implementation of ITerminal.PromptNotNull returned null.");
            }
            catch (ArgumentException e)
            {
                this._terminal.Print("An error occured with the input name:");
                this._terminal.Print(e.Message);
                this._terminal.Print("This function is cancelling.");
                return;
            }

            this._eraMigration(newName);
        }

        /// <summary>
        /// This is the unit of work to perform the actual functionality of the
        /// corresponding method.
        /// </summary>
        private void _eraMigration(Name newEraName)
        {
            throw new NotImplementedException();
            // per character
            //      add their era score to their total score
            //      Set their era score to 10 - this will make them level 1
            //      Assigns all players to the default group - is this UA or null? ua if less than level 3, ua masters otherwise

            // Takes all active-status tasks and retires them
            //      add a method to ITaskRepo to get all tasks of a certain input status

            // Players keep any in progress praxis and can publish them at no penalty
        }

        /// <summary>
        /// This method will be used to force the user to input a randomly
        /// generated number to make it harder to accidentally perform
        /// hazardous operations.
        /// </summary>
        /// <returns>True iff the user input the correct number.</returns>
        protected bool GetConfirmation(string message)
        {
            string failure = "You have failed to enter the correct number.";
            int key = new Random().Next(1000, 9999+1);
            this._terminal.Print<int>(key, message);
            if (this._terminal.Prompt<int>() != key)
            {
                this._terminal.Print(failure);
                return false;
            }
            return true;
        }

        /// <summary>
        /// This will display the various flagged tasks, praxises, comments,
        /// etc and allow for management of those items.
        /// </summary>
        public void InspectFlaggedData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This will allow admins to select tasks by their status, and then be
        /// able to change that status. Additionally, this will allow admins to
        /// edit pretired tasks.
        /// </summary>
        public void TaskMenu()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This menu will handle meta task management, such as creating and
        /// assigning meta tasks to a faction.
        /// </summary>
        public void MetaTaskMenu()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This menu will manage users. Currently, the only planned
        /// funtionality is the ability to block and delete users.
        /// </summary>
        public void UserMenu()
        {
            throw new NotImplementedException();
        }
    }
}
