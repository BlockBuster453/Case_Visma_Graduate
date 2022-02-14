using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class WorkTaskProvider : IWorkTaskProvider
    {
        private readonly AppContext _db;
        public WorkTaskProvider(AppContext db)
        {
            _db = db;
        }

        public async Task AddWorkTask(WorkTask task)
        {
            var validTime = false;
            var employee = task.Employee;
            var employeePositions = await _db.Positions
                                    .Where(p => p.Employee.Id == employee.Id)
                                    .ToArrayAsync();
            foreach (var pos in employeePositions)
            {
                // Om tidspunktet for en oppgave er i tidsrommet for minst
                // én stilling vil den være gyldig
                if (task.Date > pos.StartTime || task.Date < pos.EndTime)
                {
                    validTime = true;
                }
            }
            if (!validTime)
            {
                throw new Exception("Ingen stilling på dette tidspunktet");
            }

            await _db.WorkTasks.AddAsync(task);
            await _db.SaveChangesAsync();
        }

        public async Task<WorkTask[]> GetAll()
        {
            return await _db.WorkTasks.Include(t => t.Employee).ToArrayAsync();
        }

        public async Task<WorkTask> GetById(int id)
        {
            return await _db.WorkTasks
                    .Where(t => t.Id == id)
                    .Include(t => t.Employee)
                    .FirstAsync();
        }
    }
}