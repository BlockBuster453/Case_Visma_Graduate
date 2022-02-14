using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase
{
    public interface IWorkTaskProvider
    {
        Task<WorkTask[]> GetAll();
        Task AddWorkTask(WorkTask task);
        Task<WorkTask> GetById(int id);
    }
}