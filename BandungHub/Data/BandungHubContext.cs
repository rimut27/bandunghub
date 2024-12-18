using Microsoft.EntityFrameworkCore;
using BandungHub.Models;
using System.Collections.Generic;

namespace BandungHub.Data
{
    public class BandungHubContext : DbContext
    {
        public DbSet<Departemen> Departement { get; set; }

        public BandungHubContext(DbContextOptions<BandungHubContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departemen>(entity =>
            {
                entity.HasKey(e => e.Id); // Tetapkan sebagai primary key
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); // Atur sebagai auto-increment
            });
        }

    }
}
