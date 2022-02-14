using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VismaCase.Services
{
    public class EmployeeProvider : IEmployeeProvider
    {
        private readonly AppContext _db;
        public EmployeeProvider(AppContext db)
        {
            _db = db;
        }
    }
}