using System.Collections.Generic;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class EmployeeValidator : IEmployeeValidator
    {
        public string[] IsValid(Employee employee)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(employee.FirstName) || string.IsNullOrWhiteSpace(employee.LastName)) {
                errors.Add("Både fornavn og etternavn må fylles ut");
            }
            
            var errorArray = errors.ToArray();
            return errorArray;
        }   
    }
}