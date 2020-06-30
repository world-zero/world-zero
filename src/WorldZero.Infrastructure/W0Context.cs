using WorldZero.Domain.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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
        }


        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<CharacterModel> Characters { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
    }
}