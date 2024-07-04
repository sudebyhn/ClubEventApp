using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Domain;
using System.Reflection;
using Microsoft.Extensions.Configuration;
namespace Data.DbContextLib
{
    public class DataDb : DbContext
    {
        protected readonly IConfiguration Configuration;
        protected static string ConnectionString = string.Empty;
        public DataDb(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubMenager> ClubManagers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<SksAdmin> SksAdmin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>()
                     .HasKey(pc => new { pc.ClubId });
            modelBuilder.Entity<ClubMenager>()
                     .HasKey(pc => new { pc.ClubManagerId });
            modelBuilder.Entity<Event>()
                     .HasKey(pc => new { pc.EventId });
            modelBuilder.Entity<SksAdmin>()
                     .HasKey(pc => new { pc.SksAdminId });
            //modelBuilder.Entity<ClubMenager>().HasOne(p => p.Club)
            //           .WithOne(pc => pc.ClubMenager)
            //           .HasForeignKey<Club>(c => c.ClubId);

            //modelBuilder.Entity<Event>()
            //    .HasOne(p => p.Club)
            //    .WithMany(pc => pc.EventList)
            //    .HasForeignKey(c => c.ClubId);

        }

        public DataDb()
        {

        }

    }
}
