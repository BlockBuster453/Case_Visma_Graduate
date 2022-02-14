using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VismaCase.Services
{
    public class TaskProvider : ITaskProvider
    {
        private readonly AppContext _db;
        public TaskProvider(AppContext db)
        {
            _db = db;
        }
    }
}