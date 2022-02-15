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
            // Id defineres som key, det sørge for automatisk inkrementering
            // og vil gi exception skulle det være duplikat

            // Det er også en composite key på hver table som er satt til
            // å være unik, slik at det kommer en exception
            // om flere har samme composite key

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex("FirstName", "LastName").IsUnique();
            });
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Employee);
                entity.HasIndex("Name", "EmployeeId", "StartTime", "EndTime").IsUnique();
            });
            modelBuilder.Entity<WorkTask>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.HasOne(t => t.Employee);
                entity.HasIndex("Name", "EmployeeId", "Date").IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}