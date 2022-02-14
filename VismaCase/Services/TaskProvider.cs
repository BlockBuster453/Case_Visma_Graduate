using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VismaCase.Services
{
    private readonly AppContext _db;
    public TaskProvider(AppContext db)
    {
        _db = db;
    }
}