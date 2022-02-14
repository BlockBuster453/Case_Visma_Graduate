using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class PositionProvider : IPositionProvider
    {
        private readonly AppContext _db;
        public PositionProvider(AppContext db)
        {
            _db = db;
        }

        public async Task AddPosition(Position position)
        {
            var employee = position.Employee;
            var employeePositions = await GetPositionsForEmployee(employee);
            foreach (var pos in employeePositions)
            {
                if ((position.StartTime > pos.StartTime
                    && position.StartTime < pos.EndTime) // Ser om en stilling starter samtidig som en annen holder på
                    || (position.EndTime < pos.EndTime
                    && position.EndTime > pos.StartTime)) // Ser om en stilling avslutter samtidig som en annen holder på
                {
                    throw new Exception("Ugyldig tidspunkt for Stilling");
                }
            }

            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
        }

        public async Task<Position[]> GetAll()
        {
            return await _db.Positions
                    .Include(p => p.Employee)
                    .ToArrayAsync();
        }

        public async Task<Position> GetById(int id)
        {
            return await _db.Positions
                    .Where(p => p.Id == id)
                    .Include(p => p.Employee)
                    .FirstAsync();
        }

        public async Task<Position[]> GetPositionsForEmployee(Employee employee)
        {
            return await _db.Positions
                    .Where(p => p.Employee.Id == employee.Id)
                    .ToArrayAsync();
        }
    }
}