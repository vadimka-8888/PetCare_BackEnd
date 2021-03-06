using Microsoft.EntityFrameworkCore;

namespace PetCareBackEnd.Models
{
    public partial class PetCareEntities : DbContext
    {
        public PetCareEntities(DbContextOptions<PetCareEntities> options) 
            : base(options)  //"name=PetCareDataBaseConnection"
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Illness> Illnesses { get; set; }
        public virtual DbSet<Mention> Mentions { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Overexposure> Overexposures { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vaccination> Vaccinations { get; set; }
        public virtual DbSet<Favourite> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mention>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<Pet>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
