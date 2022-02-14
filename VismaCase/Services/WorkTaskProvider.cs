using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class TaskProvider : IWorkTaskProvider
    {
        private readonly AppContext _db;
        public TaskProvider(AppContext db)
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
            return await _db.WorkTasks.ToArrayAsync();
        }

        public async Task<WorkTask> GetById(int id)
        {
            return await _db.WorkTasks.FindAsync(id);
        }
    }
}