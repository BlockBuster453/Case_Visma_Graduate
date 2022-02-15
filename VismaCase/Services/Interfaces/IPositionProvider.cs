using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase
{
    public interface IPositionProvider
    {
        Task<Position[]> GetAll();
        Task Add(Position position);
        Task<Position> GetById(int id);
        Task<Position[]> GetPositionsForEmployee(Employee employee);
    }
}