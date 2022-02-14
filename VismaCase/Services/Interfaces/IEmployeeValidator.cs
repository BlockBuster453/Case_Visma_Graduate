using VismaCase.Models;

namespace VismaCase.Services
{
    public interface IEmployeeValidator
    {
        public string[] IsValid(Employee employee);
    }
}