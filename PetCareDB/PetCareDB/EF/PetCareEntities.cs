using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PetCareDB.EF
{
    public partial class PetCareEntities : DbContext
    {
        public PetCareEntities()
            : base("name=PetCareDataBaseConnection")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
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
