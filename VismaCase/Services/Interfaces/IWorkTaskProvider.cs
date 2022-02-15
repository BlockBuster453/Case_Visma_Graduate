using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase
{
    public interface IWorkTaskProvider
    {
        Task<WorkTask[]> GetAll();
        Task Add(WorkTask task);
        Task<WorkTask> GetById(int id);
    }
}