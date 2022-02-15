using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class WorkTaskProvider : IWorkTaskProvider
    {
        private readonly AppDbContext _db;
        public WorkTaskProvider(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(WorkTask task)
        {
            var validTime = false;
            var employee = task.Employee;
            task.EmployeeId = employee.Id;
            var pProvider = new PositionProvider(_db);
            var employeePositions = await pProvider.GetPositionsForEmployee(employee);
            if (employeePositions.Length > 0)
            {
                foreach (var pos in employeePositions)
                {
                    // Om tidspunktet for en oppgave er i tidsrommet for minst
                    // én stilling vil den være gyldig

                    if (task.Date.Ticks >= pos.StartTime.Ticks && task.Date.Ticks <= pos.EndTime.Ticks)
                    {
                        validTime = true;
                        task.Position = pos;
                        try
                        {
                            await _db.WorkTasks.AddAsync(task);
                            await _db.SaveChangesAsync();
                            return;
                        }
                        catch (Exception)
                        {
                            throw new Exception();
                        }


                    }
                }
                if (!validTime)
                {
                    throw new Exception("Ingen stilling på dette tidspunktet");
                }
            }
            else
            {
                throw new Exception("Ingen stillinger for denne ansatte");
            }
        }

        public async Task<WorkTask[]> GetAll()
        {
            return await _db.WorkTasks
                    .Include(t => t.Employee)
                    .Include(t => t.Position)
                    .ThenInclude(p => p.Employee)
                    .ToArrayAsync();
        }

        public async Task<WorkTask> GetById(int id)
        {
            return await _db.WorkTasks
                    .Where(t => t.Id == id)
                    .Include(t => t.Employee)
                    .Include(t => t.Position)
                    .ThenInclude(p => p.Employee)
                    .FirstOrDefaultAsync();
        }
    }
}