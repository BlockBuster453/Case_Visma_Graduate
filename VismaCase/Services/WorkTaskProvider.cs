using Microsoft.EntityFrameworkCore;
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