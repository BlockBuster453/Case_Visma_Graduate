using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class PositionProvider : IPositionProvider
    {
        private readonly AppDbContext _db;
        public PositionProvider(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Position position)
        {
            var employee = position.Employee;
            position.EmployeeId = employee.Id;
            var employeePositions = await GetPositionsForEmployee(employee);
            if (employeePositions.Length > 0)
            {
                foreach (var pos in employeePositions)
                {
                    if ((position.StartTime.Ticks > pos.StartTime.Ticks
                        && position.StartTime.Ticks < pos.EndTime.Ticks) // Ser om en stilling starter samtidig som en annen holder på
                        || (position.EndTime.Ticks < pos.EndTime.Ticks
                        && position.EndTime.Ticks > pos.StartTime.Ticks)
                        || (position.StartTime.Ticks < pos.StartTime.Ticks
                        && position.EndTime.Ticks > pos.EndTime.Ticks)) // Ser om en stilling avslutter samtidig som en annen holder på
                    {
                        throw new Exception("Ugyldig (Overlapper med annen stilling)");
                    }
                    else
                    {
                        try
                        {
                            await _db.Positions.AddAsync(position);
                            await _db.SaveChangesAsync();
                        }
                        catch (Exception)
                        {
                            throw new Exception();
                        }

                    }
                }
            }
            else
            {
                await _db.Positions.AddAsync(position);
                await _db.SaveChangesAsync();
            }

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
                    .FirstOrDefaultAsync();
        }

        public async Task<Position[]> GetPositionsForEmployee(Employee employee)
        {
            try
            {
                return await _db.Positions
                    .Where(p => p.Employee == employee)
                    .ToArrayAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}