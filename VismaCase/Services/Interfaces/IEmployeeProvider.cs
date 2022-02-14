using System.Collections.Generic;
using System.Threading.Tasks;
using VismaCase.Models;

namespace VismaCase
{
    public interface IEmployeeProvider
    {
        Task<Employee[]> GetAll();
        Task AddEmployee(Employee employee);
        Task<Employee> GetById(int id);
    }
}