using WorldZero.Domain.Model;
using System.Data.Entity;

namespace WorldZero.Infrastructure
{
    public class W0Context : DbContext
    {
        public W0Context() : base("name=WorldZeroDBConnectionString")
        {
            //Database.SetInitializer<W0Context>(new DropCreateDatabaseIfModelChanges<W0Context>());
            Database.SetInitializer<W0Context>(new DropCreateDatabaseAlways<W0Context>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // The validation for the fields is done by the entity classes that
            // correspond to their model.

            // Have CharacterModel.Friends and .Foes be self-referencial
            // relations.
            modelBuilder.Entity<CharacterModel>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(u => u.ToTable("Friends"));
            modelBuilder.Entity<CharacterModel>()
                .HasMany(u => u.Foes)
                .WithMany()
                .Map(u => u.ToTable("Foes"));


            // Make the usernames unique.
            modelBuilder.Entity<PlayerModel>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }


        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<CharacterModel> Characters { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<FactionModel> Factions { get; set; }
        public DbSet<EraModel> Eras { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<FlagModel> Flags { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
    }
}