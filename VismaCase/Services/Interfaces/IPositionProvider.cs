using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase
{
    public interface IPositionProvider
    {
        Task<Position[]> GetAll();
        Task AddPosition(Position position);
        Task<Position> GetById(int id);
    }
}