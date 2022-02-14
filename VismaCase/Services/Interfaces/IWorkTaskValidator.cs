using VismaCase.Models;

namespace VismaCase.Services
{
    public interface IWorkTaskValidator
    {
        public string[] IsValid(WorkTask task);
    }
}