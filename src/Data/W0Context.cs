using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Mappings;
using WorldZero.Common.ValueObject;
using System.Data.Entity;

namespace WorldZero.Data
{
    public class W0Context : DbContext
    {
        public W0Context(string connectionString="name=DefaultConnection") : base(connectionString)
        {
            // TODO: reconsider this after finishing development.
            //Database.SetInitializer<W0Context>(new DropCreateDatabaseIfModelChanges<W0Context>());
            Database.SetInitializer<W0Context>(new DropCreateDatabaseAlways<W0Context>());
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<FriendMap> Friends { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<Era> Eras { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Praxis> Praxises { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MetaTask> MetaTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder entityBuilder)
        {
            // The responsiblity of validation for the fields is done by the
            // entity classes that contain them, and this repo is only
            // responsible for enforcing rules that are inter-entity, such as
            // Names and names needing to be unique.
            this._setupCharacter(entityBuilder);
            this._setupFriends(entityBuilder);
            this._setupComment(entityBuilder);
            this._setupEra(entityBuilder);
            this._setupFaction(entityBuilder);
            this._setupFlag(entityBuilder);
            this._setupLocation(entityBuilder);
            this._setupMetaTask(entityBuilder);
            this._setupPlayer(entityBuilder);
            this._setupPraxis(entityBuilder);
            this._setupStatus(entityBuilder);
            this._setupTag(entityBuilder);
            this._setupTask(entityBuilder);
            this._setupVote(entityBuilder);

            this._setupCommentFlags(entityBuilder);
            this._setupFlagTasks(entityBuilder);
            this._setupMetaTaskFlags(entityBuilder);
            this._setupMetaTaskPraxis(entityBuilder);
            this._setupPraxisCharacters(entityBuilder);
            this._setupPraxisFlags(entityBuilder);
            this._setupTagMetaTask(entityBuilder);
            this._setupTagPraxis(entityBuilder);
            this._setupTagTasks(entityBuilder);
        }

        private void _setupCharacter(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Character>()
                .HasIndex(u => u.Name)
                .IsUnique();

            entityBuilder.Entity<Character>()
                .HasRequired(u => u.Player);
            entityBuilder.Entity<Character>()
                .HasOptional(u => u.Location);
            entityBuilder.Entity<Character>()
                .HasOptional(u => u.Faction);

            entityBuilder.Entity<Character>()
                .HasMany(u => u.Friends);
            // entityBuilder.Entity<Character>()
            //     .HasMany(u => u.Friends)
            //     .WithMany()
            //     .Map(u => u.ToTable("Friends"));
            entityBuilder.Entity<Character>()
                .HasMany(u => u.Foes)
                .WithMany()
                .Map(u => u.ToTable("Foes"));
        }

        private void _setupFriends(DbModelBuilder entityBuilder)
        {
            // TODO: make this a helper
            entityBuilder.Entity<FriendMap>()
                .HasIndex(u => new {u.LeftId, u.RightId})
                .IsUnique();

            // TODO: this needs to not cascade delete
            entityBuilder.Entity<FriendMap>()
                .HasRequired(u => u.LeftFriend);
            entityBuilder.Entity<FriendMap>()
                .HasRequired(u => u.RightFriend);
        }

        private void _setupComment(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Comment>()
                .HasIndex(u => new {u.CharacterId, u.PraxisId})
                .IsUnique();
            entityBuilder.Entity<Comment>()
                .HasRequired(u => u.Praxis);
            entityBuilder.Entity<Comment>()
                .HasRequired(u => u.Character);
        }

        private void _setupEra(DbModelBuilder entityBuilder)
        {
        }

        private void _setupFaction(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Faction>()
                .HasMany(u => u.Members);
            entityBuilder.Entity<Faction>()
                .HasMany(u => u.SponsoredTasks);
            entityBuilder.Entity<Faction>()
                .HasMany(u => u.SponsoredMetaTasks);
        }

        private void _setupFlag(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Flag>()
                .HasMany(u => u.Tasks);
            entityBuilder.Entity<Flag>()
                .HasMany(u => u.Praxises);
            entityBuilder.Entity<Flag>()
                .HasMany(u => u.Comments);
            entityBuilder.Entity<Flag>()
                .HasMany(u => u.MetaTasks);
        }
        
        private void _setupLocation(DbModelBuilder entityBuilder)
        {

            entityBuilder.Entity<Location>()
                .Property(u => u.City)
                .HasMaxLength(Name.MaxLength);
            entityBuilder.Entity<Location>()
                .Property(u => u.State)
                .HasMaxLength(Name.MaxLength);
            entityBuilder.Entity<Location>()
                .Property(u => u.Country)
                .HasMaxLength(Name.MaxLength);
            entityBuilder.Entity<Location>()
                .Property(u => u.Zip)
                .HasMaxLength(Name.MaxLength);
            entityBuilder.Entity<Location>()
                .HasIndex(u => new {u.City, u.State, u.Country, u.Zip})
                .IsUnique();

            entityBuilder.Entity<Location>()
                .HasMany(u => u.Characters);
        }

        private void _setupMetaTask(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<MetaTask>()
                .HasRequired(u => u.Faction);
            entityBuilder.Entity<MetaTask>()
                // Pretend this is required.
                .HasOptional(u => u.Status);

            entityBuilder.Entity<MetaTask>()
                .HasMany(u => u.Tags);
            entityBuilder.Entity<MetaTask>()
                .HasMany(u => u.Flags);
            entityBuilder.Entity<MetaTask>()
                .HasMany(u => u.Praxises);
        }

        private void _setupPlayer(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Player>()
                .HasIndex(u => u.Name)
                .IsUnique();

            entityBuilder.Entity<Player>()
                .HasMany(u => u.Characters);
        }

        private void _setupPraxis(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Praxis>()
                // Pretend this is required.
                .HasOptional(u => u.Task);
            entityBuilder.Entity<Praxis>()
                .HasRequired(u => u.Status);
            entityBuilder.Entity<Praxis>()
                .HasMany(u => u.Tags);
            entityBuilder.Entity<Praxis>()
                .HasMany(u => u.Flags);
            entityBuilder.Entity<Praxis>()
                .HasMany(u => u.Collaborators);
            entityBuilder.Entity<Praxis>()
                .HasMany(u => u.MetaTasks);
        }

        private void _setupStatus(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Status>()
                .HasMany(u => u.Tasks);
            entityBuilder.Entity<Status>()
                .HasMany(u => u.Praxises);
            entityBuilder.Entity<Status>()
                .HasMany(u => u.MetaTasks);
        }

        private void _setupTag(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Tag>()
                .HasMany(u => u.Tasks);
            entityBuilder.Entity<Tag>()
                .HasMany(u => u.Praxises);
            entityBuilder.Entity<Tag>()
                .HasMany(u => u.MetaTasks);
        }

        private void _setupTask(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Task>()
                .HasRequired(u => u.Faction);
            entityBuilder.Entity<Task>()
                .HasRequired(u => u.Status);
            entityBuilder.Entity<Task>()
                .HasMany(u => u.Tags);
            entityBuilder.Entity<Task>()
                .HasMany(u => u.Flags);
            entityBuilder.Entity<Task>()
                .HasMany(u => u.Praxises);
        }

        private void _setupVote(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Vote>()
                .HasIndex(u => new {u.CharacterId, u.PraxisId})
                .IsUnique();
            entityBuilder.Entity<Vote>()
                .HasRequired(u => u.Character);
            entityBuilder.Entity<Vote>()
                .HasRequired(u => u.Praxis);
        }

        private void _setupCommentFlags(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Comment>()
                .HasMany(u => u.Flags)
                .WithMany(u => u.Comments)
                .Map(u => u.ToTable("CommentFlags"));
        }

        private void _setupFlagTasks(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Flag>()
                .HasMany(u => u.Tasks)
                .WithMany(u => u.Flags)
                .Map(u => u.ToTable("FlagTasks"));
        }
        
        private void _setupMetaTaskFlags(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Flag>()
                .HasMany(u => u.MetaTasks)
                .WithMany(u => u.Flags)
                .Map(u => u.ToTable("MetaTaskFlags"));
        }

        private void _setupMetaTaskPraxis(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<MetaTask>()
                .HasMany(u => u.Praxises)
                .WithMany(u => u.MetaTasks)
                .Map(u => u.ToTable("MetaTaskPraxis"));
        }

        private void _setupPraxisCharacters(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Praxis>()
                .HasMany(u => u.Collaborators)
                .WithMany(u => u.Praxises)
                .Map(u => u.ToTable("PraxisCharacters"));
        }

        private void _setupPraxisFlags(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Flag>()
                .HasMany(u => u.Praxises)
                .WithMany(u => u.Flags)
                .Map(u => u.ToTable("PraxisFlags"));
        }

        private void _setupTagMetaTask(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<MetaTask>()
                .HasMany(u => u.Tags)
                .WithMany(u => u.MetaTasks)
                .Map(u => u.ToTable("TagMetaTask"));
        }

        private void _setupTagPraxis(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Tag>()
                .HasMany(u => u.Praxises)
                .WithMany(u => u.Tags)
                .Map(u => u.ToTable("TagPraxis"));
        }

        private void _setupTagTasks(DbModelBuilder entityBuilder)
        {
            entityBuilder.Entity<Tag>()
                .HasMany(u => u.Tasks)
                .WithMany(u => u.Tags)
                .Map(u => u.ToTable("TagTasks"));
        }
    }
}