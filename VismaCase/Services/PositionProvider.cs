using Microsoft.EntityFrameworkCore;
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
    }
}