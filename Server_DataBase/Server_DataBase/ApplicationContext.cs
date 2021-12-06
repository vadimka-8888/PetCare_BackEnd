using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server_DataBase
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Inoculation> Inoculations { get; set; }
        public DbSet<Overexposure> Overexposures { get; set; }

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options_builder)
        {
            options_builder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=PetCareInformation;Trusted_Connection=True;"); //connection string for sql server 2019
        }
    }
}
