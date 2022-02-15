using Microsoft.EntityFrameworkCore;
using VismaCase.Models;

namespace VismaCase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

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
                entity.HasOne(p => p.Employee);
            });
            modelBuilder.Entity<WorkTask>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.HasOne(t => t.Employee);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}