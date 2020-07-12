using WorldZero.Data.Model;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // The validation for the fields is done by the entity classes that
            // correspond to their model.

            // Have Character.Friends and .Foes be self-referencial
            // relations.
            modelBuilder.Entity<Character>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(u => u.ToTable("Friends"));
            modelBuilder.Entity<Character>()
                .HasMany(u => u.Foes)
                .WithMany()
                .Map(u => u.ToTable("Foes"));


            // Make the usernames unique.
            modelBuilder.Entity<Player>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }


        public DbSet<Player> Players { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<EraModel> Eras { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<PraxisModel> Praxises { get; set; }
        public DbSet<VoteModel> Votes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MetaTaskModel> MetaTasks { get; set; }
    }
}