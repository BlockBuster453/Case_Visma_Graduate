using Microsoft.EntityFrameworkCore;
using VismaCase.Models;

namespace VismaCase
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<WorkTask> WorkTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // .isUnique på en Key vil sørge for at det kommer
            // en exception om man legger inn noe med samme Key

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(p => p.Id);
            });
            modelBuilder.Entity<WorkTask>(entity =>
            {
                entity.HasKey(t => t.Id);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}