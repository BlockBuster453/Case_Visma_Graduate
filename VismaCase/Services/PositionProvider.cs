using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VismaCase.Services
{
    public class PositionProvider : IPositionProvider
    {
        private readonly AppContext _db;
        public PositionProvider(AppContext db)
        {
            _db = db;
        }
    }
}