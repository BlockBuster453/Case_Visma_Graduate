using Microsoft.EntityFrameworkCore;
using VismaCase.Models;

namespace VismaCase
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // .isUnique på en Key vil sørge for at det kommer
            // en exception om man legger inn noe med samme Key

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.Id).IsUnique();
            });
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasIndex(p => p.Id).IsUnique();
            });
            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasIndex(t => t.Id).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}