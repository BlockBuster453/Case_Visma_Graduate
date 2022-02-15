using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly AppDbContext _db;
        public EmployeeProvider(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
        }

        public async Task<Employee[]> GetAll()
        {
            return await _db.Employees.ToArrayAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _db.Employees.FindAsync(id);
        }
    }
}